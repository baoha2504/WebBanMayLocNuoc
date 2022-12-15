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
     public class SubCategoriesAdminController : Controller
     {
          private SubCategories_DAO scgDAO = new SubCategories_DAO();
          private Categories_DAO cgDAO = new Categories_DAO();
          // GET: Admin/SubCategoriesAdmin
          public ActionResult Index()
          {
               List<SubCategory> subCategories = scgDAO.GetSubCategories();
               for (int i = 0; i < subCategories.Count; i++) 
               {
                    subCategories[i].Category = cgDAO.GetCategoriesByid(subCategories[i].CategoriesID);
               }
               ViewBag.Categories = cgDAO.GetCategories();
               return View(subCategories);
          }
          public ActionResult Add(string namesubcategory, int categoryid)
          {
               SubCategory subCategory = new SubCategory();
               subCategory.CategoriesID = categoryid;
               subCategory.SubCategoriesName = namesubcategory;
               scgDAO.InsertSubCategories(subCategory);
               List<SubCategory> subCategories = scgDAO.GetSubCategories();
               for (int i = 0; i < subCategories.Count; i++)
               {
                    subCategories[i].Category = cgDAO.GetCategoriesByid(subCategories[i].CategoriesID);
               }
               return RedirectToAction("Index", subCategories);
          }
          public ActionResult Edit(int idsubcategory, string namesubcategory, int categoryid)
          {
               SubCategory subCategory = new SubCategory();
               subCategory.CategoriesID = categoryid;
               subCategory.SubCategoriesID = idsubcategory;
               subCategory.SubCategoriesName = namesubcategory;
               scgDAO.UpdateSubCategories(subCategory);
               List<SubCategory> subCategories = scgDAO.GetSubCategories();
               for (int i = 0; i < subCategories.Count; i++)
               {
                    subCategories[i].Category = cgDAO.GetCategoriesByid(subCategories[i].CategoriesID);
               }
               return RedirectToAction("Index", subCategories);
          }
     }
}