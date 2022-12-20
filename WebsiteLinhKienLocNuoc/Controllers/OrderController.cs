using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteLinhKienLocNuoc.Models;
using WebsiteLinhKienLocNuoc.DAO;

namespace WebsiteLinhKienLocNuoc.Controllers
{
    public class OrderController : Controller
    {
          // GET: Order
        private Order_DAO orderDAO = new Order_DAO();
        private Payment_DAO payDAO = new Payment_DAO();
        private Shipping_DAO shipDAO = new Shipping_DAO();
        private Customer_DAO cusDAO = new Customer_DAO();
        private Product_DAO prDAO = new Product_DAO();
          private CheckOut_DAO checkDAO = new CheckOut_DAO();
        public ActionResult OrderInformation(int orderid)
        {
            if (Session["id"] != null) 
            {
                    Order order = orderDAO.GetOrderByid(orderid);
                    if (Session["id"].ToString() == order.CustomerID.ToString())
                    {
                         ViewBag.Payment = payDAO.GetNamePayment(order.PaymentID).PaymentName;
                         ViewBag.Ship = shipDAO.GetNameShipping(order.ShippingID).ShippingName;
                         List<OrderDetail> listOrderDetail = orderDAO.GetOrderDetailByOrderid(orderid);
                         for (int i = 0; i < listOrderDetail.Count(); i++)
                         {
                              listOrderDetail[i].Product = prDAO.GetProDuctByID(listOrderDetail[i].ProductID);
                         }
                         ViewBag.OrderDetail = listOrderDetail;
                         ViewBag.OrderStatus = orderDAO.GetOrderStatusByOrderid(orderid);
                         ViewBag.newStatus = orderDAO.GetNewStatusByOrderid(orderid).OrderStatusName;
                         return View(order);
                    }
                    else
                    {
                         return RedirectToAction("Index", "Home");
                    }
            }
            else
            {
                 return RedirectToAction("Index", "Home");
            }
            
        }
          public ActionResult OrderHistory()
          {
               if (Session["id"] != null)
               {
                    int customerid = (int)Session["id"];
                    List<Order> lProssing = new List<Order>();
                    List<Order> lShipped  = new List<Order>();
                    List<Order> lCompleted = new List<Order>();
                    List<Order> lCancelled = new List<Order>();
                    List<Order> listOrder = orderDAO.GetOrderByCustomerid(customerid);
                    for( int i = 0; i< listOrder.Count;i++)
                    {
                         string newStatus = orderDAO.GetNewStatusByOrderid(listOrder[i].OrderID).OrderStatusName;
                         if(newStatus == "Đang Chờ Xác Nhận")
                         {
                              lProssing.Add(listOrder[i]);
                         }
                         else if (newStatus == "Đang Giao Hàng")
                         {
                              lShipped.Add(listOrder[i]);
                         }
                         else if (newStatus == "Đã Giao Hàng")
                         {
                              lCompleted.Add(listOrder[i]);
                         }
                         else if (newStatus == "Đã Hủy")
                         {
                              lCancelled.Add(listOrder[i]);
                         }
                    }
                    ViewBag.lProssing = lProssing;
                    ViewBag.lShipped  = lShipped ;
                    ViewBag.lCompleted = lCompleted;
                    ViewBag.lCancelled = lCancelled;
                    return View(listOrder) ;
               }
               else
               {
                    return RedirectToAction("Index", "Home");
               }
          }
          public JsonResult Cancel(int id)
          {
               OrderStatusHistory orderStatusHistory = new OrderStatusHistory();
               orderStatusHistory.OrderID = id;
               orderStatusHistory.OrderStatusName = "Đã Hủy";
               orderStatusHistory.CanceledBy = "Khách Hàng";
               checkDAO.InsertOrderHistory(orderStatusHistory);
               Message.set_flash("Hủy Đơn Hành Thành Công", "success");
               return Json(new
               {
                    status = true
               });
          }
     }
}