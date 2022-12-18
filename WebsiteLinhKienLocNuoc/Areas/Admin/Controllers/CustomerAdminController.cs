using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebsiteLinhKienLocNuoc.DAO;
using WebsiteLinhKienLocNuoc.Models;
using WebsiteLinhKienLocNuoc.Support;

namespace WebsiteLinhKienLocNuoc.Areas.Admin.Controllers
{
     public class CustomerAdminController : Controller
     {
          private Customer_DAO cusDAO = new Customer_DAO();
          WebsiteLinhKienLocNuoc.Support.Support sp = new WebsiteLinhKienLocNuoc.Support.Support();

          // GET: Admin/CustomerAdmin
          public ActionResult Index()
          { 
               List<Customer> customers = new List<Customer>();
               customers = cusDAO.GetListCustomer3();
               return View(customers);
          }
          public ActionResult Account()
          {
               List<Customer> customers = new List<Customer>();
               customers = cusDAO.GetListCustomer();
               return View(customers);
          }
          public ActionResult Edit(int idcustomer, int access, int prohibit)
          {
               Customer customer = new Customer();
               customer.Access = access;
               customer.Prohibit = prohibit;
               customer.CustomerID = idcustomer;
               cusDAO.UpdateCustomerAccess(customer);
               return RedirectToAction("Account");
          }
          public ActionResult AddAccount()
          {

               return View();
          }
          [HttpPost]
          public ActionResult AddAccount(string useremail, string userpassword, int access, int prohibit)
          {
               Customer customer = new Customer();
               customer.Email = useremail;
               customer.PassWord = sp.EncodePassword(userpassword);
               customer.Access = access;
               customer.Prohibit = prohibit;
               if (cusDAO.InsertCustomerAccess(customer) != 0)
               {
                    ViewBag.alert = "Thêm Thành Công";
               }
               else
               {
                    ViewBag.alert = "Thêm Thất Bại";
               }
               return View();
          }
     }
}