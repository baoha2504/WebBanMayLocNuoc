using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteLinhKienLocNuoc.DAO;
using WebsiteLinhKienLocNuoc.Models;
using PagedList;

namespace WebsiteLinhKienLocNuoc.Controllers
{
     public class HomeController : Controller
     {
          private Product_DAO prDAO = new Product_DAO();
          private Categories_DAO cgDAO = new Categories_DAO();
          public ActionResult Index()
          {
               ViewBag.lst1 = prDAO.GetFeatrueProDuct(1);
               ViewBag.lst2 = prDAO.GetFeatrueProDuct(5); 
               ViewBag.lst3 = prDAO.GetNewProDuct();
               return View();
          }
          public ActionResult SearchProduct(string key, int? page)
          {
               List<Product> Listproduct = prDAO.FilterProDuct(key);
               ViewBag.lst1 = Listproduct;
               if (page == null) page = 1;
               int pageSize = 8;
               int pageNumber = (page ?? 1);
               ViewBag.page = page;
               ViewBag.key = key;
               ViewBag.numResult = Listproduct.Count();
               return View(Listproduct.ToPagedList(pageNumber, pageSize));

          }
          public ActionResult AdvancedSearch(int? category_1, int? category_2, int? category_3, int? category_4, int? category_5, int? category_6, int? category_7, int? category_8, string brand_1, string brand_2, string brand_3, int valuemin, int valuemax, int? page)
          {
               List<Product> Listproduct = prDAO.FilterProDuct("lõi");
               ViewBag.lst1 = Listproduct;
               if (page == null) page = 1;
               int pageSize = 8;
               int pageNumber = (page ?? 1);
               ViewBag.page = page;
               ViewBag.key = "key";
               ViewBag.numResult = Listproduct.Count();
               return View(Listproduct.ToPagedList(pageNumber, pageSize));

          }
          [HttpGet]
          public ActionResult Search(string search, string submit_search)
          {
               return RedirectToAction("SearchProduct", "Home", new { key = search });
          }
          public ActionResult DeliveryPolicy()
          {

               return View();
          }
          public ActionResult WarrantyPolicy()
          {

               return View();
          }
          public ActionResult ReturnPolicy()
          {

               return View();
          }
          public ActionResult ContactUs()
          {

               return View();
          }
     }
}