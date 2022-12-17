using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebsiteLinhKienLocNuoc.DAO;
using WebsiteLinhKienLocNuoc.Models;

namespace WebsiteLinhKienLocNuoc.Areas.Admin.Controllers
{
     public class OrderAdminController : Controller
     {
          public const string SessionOrder = "SessionOrder";
          Order_DAO orderDAO = new Order_DAO();
          Customer_DAO cusDAO = new Customer_DAO();
          CheckOut_DAO checkDAO = new CheckOut_DAO();
          Payment_DAO payDAO = new Payment_DAO();
          Shipping_DAO shipDAO = new Shipping_DAO(); 
          Product_DAO proDAO = new Product_DAO();
          List<Cart_item> _Items = new List<Cart_item>();
          // GET: Admin/OrderAdmin
          public ActionResult Index()
          {
               List<Order> orders = orderDAO.GetOrderData();
               List<OrderStatusHistory> orderStatuses = new List<OrderStatusHistory>();
               List<Customer> customers = new List<Customer>();
               for (int i = 0; i < orders.Count; i++)
               {
                    OrderStatusHistory orderStatus = orderDAO.GetNewStatusByOrderid(orders[i].OrderID);
                    orderStatuses.Add(orderStatus);
                    Customer customer = cusDAO.GetCustomerbyID(orders[i].CustomerID);
                    customers.Add(customer);
               }
               ViewBag.orders = orders;
               ViewBag.orderStatuses = orderStatuses;
               return View();
          }
          public ActionResult Approve()
          {
               List<Order> orders = new List<Order>();
               orders = orderDAO.GetOrderData();
               List<Order> orders1 = new List<Order>();
               for (int i = 0; i < orders.Count; i++)
               {
                    OrderStatusHistory orderStatus = orderDAO.GetNewStatusByOrderid(orders[i].OrderID);
                    if (orderStatus.OrderStatusName == "Đang Chờ Xác Nhận") { orders1.Add(orders[i]); }

               }
               List<Customer> customers = new List<Customer>();
               for (int i = 0; i < orders1.Count; i++)
               {
                    Customer customer = cusDAO.GetCustomerbyID(orders1[i].CustomerID);
                    customers.Add(customer);
               }
               _Items = new List<Cart_item>();
               Session["SessionOrder"] = _Items;
               ViewBag.orders = orders1;
               return View();
          }
          public JsonResult UpdateStatus(int id, string status)
          {
               OrderStatusHistory orderStatusHistory = new OrderStatusHistory();
               orderStatusHistory.OrderID = id;
               orderStatusHistory.OrderStatusName = status;
               orderStatusHistory.CanceledBy = "Người Bán";
               checkDAO.InsertOrderHistory(orderStatusHistory);
               return Json(new
               {
                    status = true
               });
          }

          public ActionResult AddOrder()
          {
               ViewBag.Customers = cusDAO.GetListCustomer();
               ViewBag.Ships = shipDAO.GetListShipping();
               ViewBag.Pays = payDAO.GetListPayment();
               ViewBag.Products = proDAO.GetProduct();
               return View();
          }
          [HttpGet]
          public ActionResult InsertAddOrder(int customerid, string name, string phone, string address, int shipping, int pay, string note)
          {
               Order order = new Order();
               order.CustomerID = customerid;
               order.ShippingID = shipping;
               order.PaymentID = pay;
               order.ShippingAddress = address;
               order.Name = name;
               order.Phone = phone;
               order.Note = note;
               order.Discount = 0;
               int total = 0;
               List<Cart_item> listCart = (List<Cart_item>)Session[SessionOrder];
               for (int i = 0; i < listCart.Count(); i++)
               {
                    total += listCart[i].Price * listCart[i].Quantity;

               }
               if (shipping == 2)
               {
                    order.ShippingFee = 30000;
                    order.Total = total + order.ShippingFee;
               }
               else
               {
                    order.ShippingFee = 0;
                    order.Total = total;
               }
               int orderid = checkDAO.InsertOrder(order);
               InsertOrderHistory(orderid);
               InsertOrderDetail(orderid);
               return RedirectToAction("Index", "Admin/OrderAdmin");
          }
          public void InsertOrderHistory(int orderid)
          {
               OrderStatusHistory orderStatusHistory = new OrderStatusHistory();
               orderStatusHistory.OrderStatusName = "Đang Chờ Xác Nhận";
               orderStatusHistory.OrderID = orderid;
               orderStatusHistory.CanceledBy = "";
               checkDAO.InsertOrderHistory(orderStatusHistory);
          }
          public void InsertOrderDetail(int orderid)
          {
               List<Cart_item> listCart = (List<Cart_item>)Session[SessionOrder];
               for (int i = 0; i < listCart.Count(); i++)
               {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderID = orderid;
                    orderDetail.ProductID = listCart[i].ProductID;
                    orderDetail.Quantity = listCart[i].Quantity;
                    Product product = proDAO.GetProDuctByID(listCart[i].ProductID);
                    orderDetail.Price = product.PriceNew;
                    checkDAO.InsertOrderDetail(orderDetail);
               }
               Session[SessionOrder] = new List<Cart_item>();
          }
          public JsonResult AddProductToOrder(int productid, int number, int price)
          {
               var sessionOrder = (List<Cart_item>)Session[SessionOrder];//lấy danh sách các sản phẩm trong session

               if (sessionOrder != null)
               {
                    var list = (List<Cart_item>)sessionOrder;
                    if (sessionOrder.Exists(x => x.ProductID == productid))
                    {
                         var quantity1 = 0;
                         int priceTotal = 0;
                         int temprice = 0;
                         foreach (var item in sessionOrder)
                         {
                              if (item.ProductID == productid)
                              {
                                   item.Quantity += number;
                                   quantity1 = item.Quantity;
                                   temprice = item.Quantity * item.Price;
                              }

                         }
                         if (sessionOrder != null)
                         {
                              list = (List<Cart_item>)sessionOrder;
                              foreach (var item1 in list)
                              {

                                   int temp = ((int)item1.Price) * ((int)item1.Quantity);
                                   priceTotal += temp;

                              }
                         }
                         return Json(new
                         {
                              status = true,
                              quantity = quantity1,
                              productid = productid,
                              subtotal = String.Format("{0:0,0}", priceTotal),
                              subitemtotal = String.Format("{0:0,0}", temprice),
                              temprice = String.Format("{0:0,0}", temprice),
                              method = "exist"
                         });
                    }
                    else
                    {
                         int priceTotal = 0;
                         Cart_item item = new Cart_item();
                         item.product = proDAO.GetProDuctByID(productid);
                         item.ProductID = productid;
                         item.Quantity = number;
                         item.Price = price;
                         list.Add(item);
                         Session[SessionOrder] = list;
                         if (sessionOrder != null)
                         {
                              list = (List<Cart_item>)sessionOrder;
                              foreach (var item1 in list)
                              {

                                   int temp = ((int)item1.Price) * ((int)item1.Quantity);
                                   priceTotal += temp;

                              }
                         }
                         return Json(new
                         {
                              status = true,
                              productid = item.ProductID,
                              productname = item.product.ProductName,
                              price = String.Format("{0:0,0}", item.product.PriceNew),
                              quantity = item.Quantity,
                              subtotal = String.Format("{0:0,0}", priceTotal),
                              subitemtotal = String.Format("{0:0,0}", item.product.PriceNew * item.Quantity),
                              method = "add"
                         });
                    }

               }
               else
               {
                    int priceTotal = 0;
                    Cart_item item = new Cart_item();
                    item.product = proDAO.GetProDuctByID(productid);
                    item.ProductID = productid;
                    item.Quantity = number;
                    item.Price = price;
                    var list = new List<Cart_item>();

                    list.Add(item);
                    Session[SessionOrder] = list;
                    if (list != null)
                    {
                         foreach (var item1 in list)
                         {
                              int temp = ((int)item1.Price) * ((int)item1.Quantity);
                              priceTotal += temp;
                         }
                    }
                    return Json(new
                    {
                         status = true,
                         productid = item.ProductID,
                         productname = item.product.ProductName,
                         price = String.Format("{0:0,0}", item.product.PriceNew),
                         subitemtotal = String.Format("{0:0,0}", item.product.PriceNew * item.Quantity),
                         quantity = item.Quantity,
                         subtotal = String.Format("{0:0,0}", priceTotal),
                         method = "add"
                    });

               }

          }
          public ActionResult EditOrder(int orderid)
          {
               Order order = orderDAO.GetOrderByid(orderid);
               order.Shipping = shipDAO.GetNameShipping(order.ShippingID);
               order.Payment = payDAO.GetNamePayment(order.PaymentID);
               List<OrderDetail> listOrderDetail = orderDAO.GetOrderDetailByOrderid(orderid);
               for (int i = 0; i < listOrderDetail.Count(); i++)
               {
                    listOrderDetail[i].Product = proDAO.GetProDuctByID(listOrderDetail[i].ProductID);
               }
               ViewBag.OrderDetail = listOrderDetail;
               ViewBag.OrderStatusHistory = orderDAO.GetOrderStatusByOrderid(orderid);
               ViewBag.Ships = shipDAO.GetListShipping();
               ViewBag.Pays = payDAO.GetListPayment();
               return View(order);
          }
          public JsonResult RemoveProductToOrder(int id)
          {
               var sessionOrder = (List<Cart_item>)Session[SessionOrder];//lấy danh sách các sản phẩm trong giỏ hàng hiện có
               sessionOrder.RemoveAll(x => x.ProductID == id);

               Session[SessionOrder] = sessionOrder;
               var list = (List<Cart_item>)sessionOrder;
               int priceTotal = 0;
               foreach (var item1 in list)
               {
                    int temp = ((int)item1.Price) * ((int)item1.Quantity);
                    priceTotal += temp;
               }
               return Json(new
               {

                    status = true,
                    productid = id,
                    subtotal = String.Format("{0:0,0}", priceTotal)
               });
          }
     }

}