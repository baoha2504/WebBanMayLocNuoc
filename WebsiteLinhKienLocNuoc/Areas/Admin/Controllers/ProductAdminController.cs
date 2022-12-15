using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebsiteLinhKienLocNuoc.DAO;
using WebsiteLinhKienLocNuoc.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace WebsiteLinhKienLocNuoc.Areas.Admin.Controllers
{
    public class ProductAdminController : Controller
    {
        private Product_DAO prDAO = new Product_DAO();
        private SubCategories_DAO scgDAO = new SubCategories_DAO();
        WebsiteLinhKienLocNuoc.Support.Support sp = new WebsiteLinhKienLocNuoc.Support.Support();
        // GET: Admin/Product
        public ActionResult Index() 
        {
            List<Product> products = prDAO.GetProduct();
            return View(products);
        }
          public ActionResult AddProduct()
          {
               List<SubCategory> listSubCate = scgDAO.GetSubCategories();
               return View(listSubCate);
          }
          [HttpPost]
          [ValidateInput(false)]
          public ActionResult AddProduct( int subcate,HttpPostedFileBase file,string productname,string brand,string model,string suppiler,string pricenew,string priceold,string summary,string area)
          {
               // lấy tên ảnh
               string filename = file.FileName.ToString();
               //lấy đuôi ảnh
               string ExtensionFile = sp.GetFileExtension(filename);
               // lấy tên sản phẩm làm slug

               //lấy tên mới của ảnh + [đuôi ảnh lấy đc]
               string namefilenew = productname + "-300x300" + "." + ExtensionFile;
               //lưu ảnh vào đường đẫn
               var path = Path.Combine(Server.MapPath("~/Public/image/demo"), namefilenew);
               //nếu thư mục k tồn tại thì tạo thư mục
               var folder = Server.MapPath("~/Public/image/demo");
               if (!Directory.Exists(folder))
               {
                    Directory.CreateDirectory(folder);
               }
               file.SaveAs(path);
               System.Web.Helpers.WebImage img = new System.Web.Helpers.WebImage(path);
               img.Resize(301, 301, false);
               img.Crop(1, 1, 0, 0);
               img.Save(path);
               Product product = new Product();
               product.ProductName = productname;
               product.SubCategoriesID = subcate;
               product.Brand = brand;
               product.Model = model;
               product.Suppiler = suppiler;
               product.PriceNew = int.Parse(pricenew.Replace(",", ""));
               product.PriceOld = int.Parse(priceold.Replace(",", ""));
               product.Summary = summary;
               product.Description = area;
               product.Status = 1;
               product.Image = namefilenew;
               prDAO.InsertProduct(product);
               List<SubCategory> listSubCate = scgDAO.GetSubCategories();
               ViewBag.alert = "Thêm thành công"; 
               return View(listSubCate);
          }
          public ActionResult EditProduct(int productid)
          {
               Product product = prDAO.GetProDuctByID(productid);
               product.SubCategory = scgDAO.GetSubCategoriesByid(product.SubCategoriesID);
               ViewBag.product = product;
               List<SubCategory> listSubCate = scgDAO.GetSubCategories();
               return View(listSubCate);
          }
          [HttpPost]
          [ValidateInput(false)]
          public ActionResult EditProduct(int id,int subcate, HttpPostedFileBase file, string productname, string brand, string model, string suppiler, string pricenew, string priceold, string summary, string area)
          {
               Product product = prDAO.GetProDuctByID(id);
               file = Request.Files["file"];
               string filename = file.FileName.ToString();
               if (filename.Equals("") == false)
               {
                    //lấy đuôi ảnh
                    string ExtensionFile = sp.GetFileExtension(filename);
                    // lấy tên sản phẩm làm slug

                    //lấy tên mới của ảnh + [đuôi ảnh lấy đc]
                    string namefilenew = productname + "-300x300" + "." + ExtensionFile;
                    //lưu ảnh vào đường đẫn
                    var path = Path.Combine(Server.MapPath("~/Public/image/demo"), namefilenew);
                    //nếu thư mục k tồn tại thì tạo thư mục
                    var folder = Server.MapPath("~/Public/image/demo");
                    if (!Directory.Exists(folder))
                    {
                         Directory.CreateDirectory(folder);
                    }
                    file.SaveAs(path);
                    System.Web.Helpers.WebImage img = new System.Web.Helpers.WebImage(path);
                    img.Resize(302, 302, false);
                    img.Crop(1, 1, 1, 1);
                    img.Save(path);
                    product.Image = namefilenew;
               }
               product.ProductName = productname;
               product.SubCategoriesID = subcate;
               product.Brand = brand;
               product.Model = model;
               product.Suppiler = suppiler;
               product.PriceNew = int.Parse(pricenew.Replace(",", ""));
               product.PriceOld = int.Parse(priceold.Replace(",", ""));
               product.Summary = summary;
               product.Description = area;
               prDAO.UpdateProduct(product);
               return RedirectToAction("EditProduct",new { productid = id });
          }
          public JsonResult UpdateStatus(int productid, int status)
          {
               Product product = prDAO.GetProDuctByID(productid);
               product.Status = status;
               if (prDAO.UpdateStatus(product) != 0)
               {
                    return Json(new
                    {
                         status = true
                    });
               }else return Json(new
               {
                    status = false
               });
          }
          public JsonResult GetPriceProduct(int productid)
          {
               Product product = prDAO.GetProDuctByID(productid);
               
               if (product != null)
               {
                    return Json(new
                    {
                         status=true,
                         price = product.PriceNew
                    });
               }
               else return Json(new
               {
                    status = false
               });
          }

     }
}