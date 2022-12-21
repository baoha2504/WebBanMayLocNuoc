using System;
using System.Collections.Generic;
using System.Linq;
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
        Categories_DAO cateDAO = new Categories_DAO();
        SubCategories_DAO subDAO = new SubCategories_DAO();
        Review_DAO rvDao = new Review_DAO();
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
            ViewBag.Customers = cusDAO.GetListCustomer3().Count();
            ViewBag.Staff = cusDAO.GetListCustomer2().Count();
            ViewBag.Admin = cusDAO.GetListCustomer1().Count();
            ViewBag.Products = proDAO.GetProduct().Count();

            if (orderDAO.GetRevenueDay() != String.Empty)
            {
                ViewBag.RevenueDay = string.Format("{0:0,0}", Convert.ToDecimal(orderDAO.GetRevenueDay()));
            } else
            {
                ViewBag.RevenueDay = "0";
            }

            if (orderDAO.GetRevenueMonth() != String.Empty)
            {
                ViewBag.RevenueMonth = string.Format("{0:0,0}", Convert.ToDecimal(orderDAO.GetRevenueMonth()));
            }
            else
            {
                ViewBag.RevenueMonth = "0";
            }

            if (orderDAO.GetRevenueYear() != String.Empty)
            {
                ViewBag.RevenueYear = string.Format("{0:0,0}", Convert.ToDecimal(orderDAO.GetRevenueYear()));
            }
            else
            {
                ViewBag.RevenueYear = "0";
            }

            if (rvDao.GetReviewCount() != String.Empty)
            {
                ViewBag.ReviewMonth = rvDao.GetReviewCount();
            }
            else
            {
                ViewBag.ReviewMonth = "0";
            }

            if (orderDAO.GetCountOderInWeek() != String.Empty)
            {
                ViewBag.CountOderInWeek = orderDAO.GetCountOderInWeek();
            }
            else
            {
                ViewBag.CountOderInWeek = "0";
            }
            if (orderDAO.GetCountOderInLastWeek() != String.Empty)
            {
                ViewBag.CountOderInLastWeek = orderDAO.GetCountOderInLastWeek();
            }
            else
            {
                ViewBag.CountOderInLastWeek = "0";
            }
            if (orderDAO.GetTotalMoneyOfMachineInMonth() != String.Empty)
            {
                ViewBag.TotalMoneyOfMachineInMonth = string.Format("{0:0,0}", Convert.ToDecimal(orderDAO.GetTotalMoneyOfMachineInMonth()));
            }
            else
            {
                ViewBag.TotalMoneyOfMachineInMonth = "0";
            }
            ViewBag.Categories = cateDAO.GetCategories().Count();
            //ViewBag.Subcategories = subDAO.GetListSubCategories().Count();
            ViewBag.Subcategories = subDAO.GetSubCategories().Count();
            return View();
        }
    }
}