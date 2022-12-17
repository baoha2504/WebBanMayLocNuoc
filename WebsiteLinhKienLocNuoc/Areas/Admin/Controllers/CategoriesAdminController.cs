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
    public class CategoriesAdminController : Controller
    {
          Categories_DAO cagDAO = new Categories_DAO();
        // GET: Admin/CategoriesAdmin
        public ActionResult Index()
        {
            List<Category> categories = cagDAO.GetCategories();
            return View(categories); 
         }
          public ActionResult Add(string namecategory)
          {
               Category category = new Category();
               category.CategoriesName = namecategory;
               cagDAO.InsertCategories(category);
               return RedirectToAction("Index");
          }
          public ActionResult Edit(int idcategory,string namecategory)
          {
               Category category = new Category();
               category.CategoriesID = idcategory;
               category.CategoriesName = namecategory;
               cagDAO.UpdateCategories(category);
               return RedirectToAction("Index");
          }
     }
}