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
    public class SubCategoriesController : Controller
    {
          // GET: SubCategories
        private Product_DAO prDAO = new Product_DAO();
        private SubCategories_DAO scgDAO = new SubCategories_DAO();
        private Categories_DAO cgDAO = new Categories_DAO();
        public ActionResult SubCategories(int id,int? page)
        {
               if (scgDAO.GetSubCategoriesByid(id) != null)
               {
                    SubCategory subCategory = scgDAO.GetSubCategoriesByid(id);
                    ViewBag.subCategory = subCategory; 
                    if (subCategory != null)
                    {
                         ViewBag.cAtegory = cgDAO.GetCategoriesByid(subCategory.CategoriesID);
                    }
                    List<Product> lstproduct = prDAO.GetProDuctBySubCategory(id);
                    ViewBag.lstproduct = lstproduct;
                    ViewBag.lstfeaproduct = prDAO.GetFeatrueProDuct(5);

                    if (page == null) page = 1;
                    int pageSize = 9;
                    int pageNumber = (page ?? 1);
                    ViewBag.page = page;
                    return View(lstproduct.ToPagedList(pageNumber, pageSize));
               }
               else
               {
                    return RedirectToAction("Index", "Home");
               }
          }
    }
}