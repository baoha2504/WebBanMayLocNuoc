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
    public class CategoriesController : Controller
    {
          // GET: Categories
        private Product_DAO prDAO = new Product_DAO();
        private Categories_DAO cgDAO = new Categories_DAO();
        public ActionResult Categories(int id ,int? page)
        {
            if(cgDAO.GetCategoriesByid(id) != null)
            { 
                    ViewBag.sp = cgDAO.GetCategoriesByid(id);
                    List<Product> lst1 = prDAO.GetProDuctByCategory(id);
                    ViewBag.lst1 = lst1;
                    ViewBag.lst2 = prDAO.GetFeatrueProDuct(5);
                    if (page == null) page = 1;
                    int pageSize = 9;
                    int pageNumber = (page ?? 1);
                    ViewBag.page = page;
                    return View(lst1.ToPagedList(pageNumber, pageSize));
            }
            else return RedirectToAction("Index","Home");
          }
      
    }
}