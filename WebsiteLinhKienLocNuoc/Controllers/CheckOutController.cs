using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebsiteLinhKienLocNuoc.DAO;
using WebsiteLinhKienLocNuoc.Models;

namespace WebsiteLinhKienLocNuoc.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        private Customer_DAO cusDAO = new Customer_DAO();
        private CheckOut_DAO checkDAO = new CheckOut_DAO();
        private Cart_DAO cartDAO = new Cart_DAO();
        private Product_DAO proDAO = new Product_DAO();
        private Voucher_DAO vcDAO = new Voucher_DAO();
        public ActionResult Index()
        {
            if (Session["id"] != null)
            {
                Customer customer = cusDAO.GetCustomerbyID((int)Session["id"]);
                var sessionCart = Session["SessionCart"];
                var list = new List<Cart_item>();
                int priceTotal = 0;
                if (sessionCart != null)
                {
                    list = (List<Cart_item>)sessionCart;
                    foreach (var item1 in list)
                    {

                        int temp = ((int)item1.Price) * ((int)item1.Quantity);
                        priceTotal += temp;
                    }
                }
                ViewBag.CartTotal = priceTotal;
                ViewBag.CartNum = list.Count();
                if (Session["discount"] == null)
                {
                    ViewBag.Discount = 0;
                } else
                {
                    ViewBag.Discount = Session["discount"];
                }
                if (Session["priceAfterDiscount"] == null)
                {
                    ViewBag.PriceAfterDiscount = priceTotal;
                } else
                {
                    ViewBag.PriceAfterDiscount = Session["priceAfterDiscount"];
                }
                ViewBag.Voucher = Session["vouchercode"];
                ViewBag.list = (List<Cart_item>)sessionCart;
                return View(customer);
            }
            else
            {
                TempData["Error"] = "Bạn cần phải đăng nhập";
                Message.set_flash("Bạn cần phải đăng nhập", "error");
                return RedirectToAction("Login", "Account");
            }

        }
        public ActionResult CheckDiscount(string voucher, string pricecheck)
        {
            Voucher vouchers = vcDAO.GetVoucherByCode(voucher);
            int priceAfterDiscount;
            int discount;
            Session["vouchercode"] = voucher;
            //int price;
            if (vouchers != null)
            {
                if (vouchers.MiximunVal > Int32.Parse(pricecheck))
                {
                    return RedirectToAction("Index", "Checkout");
                }
                else
                {
                    discount = Int32.Parse(pricecheck) * (int)vouchers.SalePercent / 100;
                    if (discount > (int)vouchers.MaximumDis)
                    {
                        discount = (int)vouchers.MaximumDis;
                    }
                    priceAfterDiscount = Int32.Parse(pricecheck) - discount;
                    Session["discount"] = discount;
                    Session["priceAfterDiscount"] = priceAfterDiscount;
                    return RedirectToAction("Index", "Checkout");
                }
            }
            else
            {
                Session["discount"] = 0;
                Session["priceAfterDiscount"] = Int32.Parse(pricecheck);
                TempData["Error"] = "Voucher không tồn tại";
                return RedirectToAction("Index", "Checkout");
            }
        }
        [HttpPost]
        public JsonResult Payment(string shipid, string paymentid, string shipadd, string discounting, string total, string note, string name, string phone)
        {

            Order order = new Order();
            order.CustomerID = (int)(Session["id"]);
            order.ShippingID = Int32.Parse(shipid);
            order.PaymentID = Int32.Parse(paymentid);
            order.ShippingAddress = shipadd;
            order.Name = name;
            order.Phone = phone;
            order.Note = note;
            if (discounting != string.Empty)
            {
                order.Discount = Int32.Parse(discounting);
            }
            else
            {
                order.Discount = 0;
            }
            if (Int32.Parse(shipid) == 2)
            {
                order.ShippingFee = 30000;
                order.Total = Int32.Parse(total) + order.ShippingFee;
            }
            else
            {
                order.ShippingFee = 0;
                order.Total = Int32.Parse(total);
            }
            int orderid = checkDAO.InsertOrder(order);
            InsertOrderHistory(orderid);
            InsertOrderDetail(orderid);
            Session["discount"] = null;
            Session["priceAfterDiscount"] = null;
            Session["vouchercode"] = null;
            Message.set_flash("Đặt hàng thành công", "success");
            return Json(new
            {
                status = true

            });
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
            List<Cart> listCart = cartDAO.GetCartByCustomerID((int)(Session["id"]));
            for (int i = 0; i < listCart.Count(); i++)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderID = orderid;
                orderDetail.ProductID = listCart[i].ProductID;
                orderDetail.Quantity = listCart[i].Quantity;
                Product product = proDAO.GetProDuctByID(listCart[i].ProductID);
                orderDetail.Price = product.PriceNew;
                checkDAO.InsertOrderDetail(orderDetail);
                cartDAO.DeleteCart(listCart[i]);
            }
            Session["SessionCart"] = new List<Cart_item>();
        }
    }
}