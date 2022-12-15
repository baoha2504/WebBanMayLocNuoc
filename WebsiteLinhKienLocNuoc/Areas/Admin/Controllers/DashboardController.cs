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
     public class DashboardController : Controller
     {
          Order_DAO orderDAO = new Order_DAO();
          Customer_DAO cusDAO = new Customer_DAO();
          Product_DAO proDAO = new Product_DAO();
          // GET: Admin/Dashboard
          public ActionResult Index()
          {
               List<Order> orders = new List<Order>();
               orders = orderDAO.GetOrderData(); 
               List<Order> orders1 = new List<Order>();
               List<Order> orders2 = new List<Order>();
               for (int i = 0; i < orders.Count; i++)
               {
                    OrderStatusHistory orderStatus = orderDAO.GetNewStatusByOrderid(orders[i].OrderID);
                    if (orderStatus.OrderStatusName == "Đang Chờ Xác Nhận") { orders1.Add(orders[i]); }
                    if ((orders[i].DateAdd.Value.DayOfYear) == DateTime.Now.DayOfYear) { orders2.Add(orders[i]); }
               }

               ViewBag.Order = orders1.Count();
               ViewBag.Order2 = orders2.Count();
               ViewBag.Customers = cusDAO.GetListCustomer().Count();
               ViewBag.Products = proDAO.GetProduct().Count();
               return View();
          }
     }
}