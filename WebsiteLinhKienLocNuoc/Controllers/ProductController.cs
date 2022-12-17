using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteLinhKienLocNuoc.DAO;
using WebsiteLinhKienLocNuoc.Models;

namespace WebsiteLinhKienLocNuoc.Controllers
{
    public class ProductController : Controller
    {
        // GET: Procduct
        private Product_DAO prDAO = new Product_DAO();
        private SubCategories_DAO scgDAO = new SubCategories_DAO();
        private Review_DAO reDAO = new Review_DAO();
        private Customer_DAO cusDAO = new Customer_DAO();
        public ActionResult QuickView(int id)
        {
            Product product = prDAO.GetProDuctByID(id);
            return View(product);
        }
        public ActionResult DetailProduct(int id)
        { 
               if (prDAO.GetProDuctByID(id) != null)
               {
                    Product product = prDAO.GetProDuctByID(id);
                    SubCategory subCategory = scgDAO.GetSubCategoriesByid(product.SubCategoriesID);
                    ViewBag.subName = subCategory.SubCategoriesName;
                    ViewBag.categoriesID = subCategory.CategoriesID;
                    ViewBag.relateProduct = prDAO.GetRelateProDuct(subCategory.SubCategoriesID);
                    List<Review> listReview = reDAO.GetReviewByProductid(id);
                    for (int i = 0; i < listReview.Count(); i++)
                    {
                         listReview[i].Customer = cusDAO.GetCustomerbyID(listReview[i].CustomerID);
                    }
                    ViewBag.listReview = listReview;
                    return View(product);
               }
               else
               {
                    return RedirectToAction("Index", "Home");
               }
          }

          public JsonResult Comment(int id, string comment, int nstar)
          {
               if(Session["id"] == null)
               {
                    Message.set_flash("Bạn cần đăng nhập để bình luận", "error");
                    return Json(new
                    {
                         status = false

                    }); 
               }
               else
               {
                    Review review = new Review();
                    review.CustomerID = (int)Session["id"];
                    review.ProductID = id;
                    review.Comment = comment;
                    review.NumStar = (int)nstar;
                    reDAO.InsertReview(review);
                    return Json(new
                    {
                         status = true
                    });
               }
               
          }
     }
}