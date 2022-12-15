using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteLinhKienLocNuoc.DAO;
using WebsiteLinhKienLocNuoc.Models;
using System.Web.Script.Serialization;

namespace WebsiteLinhKienLocNuoc.Controllers
{
     public class CartController : Controller
     {
          // GET: Cart
          public const string SessionCart = "SessionCart";
          private Product_DAO prDAO = new Product_DAO();
          private Cart_DAO cartDAO = new Cart_DAO();
          [ChildActionOnly] 
          public ActionResult CartPartial()
          {
               var sessionCart = Session[SessionCart];
               var list = new List<Cart_item>();
               int priceTotal = 0;
               if (sessionCart != null)
               {
                    list = (List<Cart_item>)sessionCart;
                    foreach (var item1 in list)
                    {

                         int temp = ((int)item1.Price) * ((int)item1.Quantity);
                         priceTotal += temp;
                    }
               }
               ViewBag.CartTotal = priceTotal;
               ViewBag.CartNum = list.Count();
               ViewBag.list = (List<Cart_item>)sessionCart;
               return View();
          }
          public ActionResult Index()
          {
               var sessionCart = Session[SessionCart];
               var list = new List<Cart_item>();
               int priceTotal = 0;
               if (sessionCart != null)
               {
                    list = (List<Cart_item>)sessionCart;
                    foreach (var item1 in list)
                    {

                         int temp = ((int)item1.Price) * ((int)item1.Quantity);
                         priceTotal += temp;
                    }
               }
               ViewBag.CartTotal = priceTotal;
               ViewBag.CartNum = list.Count();
               ViewBag.list = (List<Cart_item>)sessionCart;
               return View();

          }
          [HttpPost]
          public JsonResult Add(string cartModel)
          {
               var cart = new JavaScriptSerializer().Deserialize<List<Cart_item>>(cartModel);
               var sessionCart = (List<Cart_item>)Session[SessionCart];//lấy danh sách các sản phẩm trong giỏ hàng hiện có

               if (sessionCart != null)
               {
                    var list = (List<Cart_item>)sessionCart;
                    if (list.Exists(x => x.ProductID == cart[0].ProductID))
                    {
                         var quantity1 = 0;
                         int priceTotal = 0;

                         foreach (var item in list)
                         {
                              if (item.ProductID == cart[0].ProductID)
                              {
                                   item.Quantity += cart[0].Quantity;
                                   quantity1 = item.Quantity;
                                   if (Session["id"] != null)
                                   {
                                        Cart icart = new Cart();
                                        icart.ProductID = item.ProductID;
                                        icart.Quantity = item.Quantity;
                                        icart.CustomerID = (int)Session["id"];
                                        cartDAO.UpdateCart(icart);
                                   }
                              }

                         }
                         if (sessionCart != null)
                         {
                              list = (List<Cart_item>)sessionCart;
                              foreach (var item1 in list)
                              {

                                   int temp = ((int)item1.Price) * ((int)item1.Quantity);
                                   priceTotal += temp;

                              }
                         }
                         return Json(new
                         {
                              status = true,
                              Count = list.Count(),
                              quantity = quantity1,
                              productid = cart[0].ProductID,
                              subtotal = priceTotal.ToString("N0"),
                              method = "exist"
                         });
                    }
                    else
                    {
                         var tmp = new Cart_item();

                         tmp.ProductID = cart[0].ProductID;
                         tmp.product = prDAO.GetProDuctByID(cart[0].ProductID);
                         tmp.Price = (int)prDAO.GetProDuctByID(cart[0].ProductID).PriceNew;
                         tmp.Quantity = cart[0].Quantity;
                         list.Add(tmp);
                         Session[SessionCart] = list;
                         if (Session["id"] != null)
                         {
                              Cart icart = new Cart();
                              icart.ProductID = tmp.ProductID;
                              icart.Quantity = tmp.Quantity;
                              icart.CustomerID = (int)Session["id"];
                              cartDAO.InsertCart(icart);
                         }
                         int priceTotal = 0;
                         if (sessionCart != null)
                         {
                              list = (List<Cart_item>)sessionCart;
                              foreach (var item1 in list)
                              {

                                   int temp = ((int)item1.Price) * ((int)item1.Quantity);
                                   priceTotal += temp;
                              }
                         }
                         return Json(new
                         {
                              status = true,
                              Count = list.Count(),
                              productid = tmp.ProductID,
                              image = tmp.product.Image,
                              name = tmp.product.ProductName,
                              quantity = tmp.Quantity,
                              price = ((int)tmp.product.PriceNew).ToString("N0"),
                              subtotal = priceTotal.ToString("N0"),
                              method = "add"
                         });
                    }

               }
               else
               {
                    var tmp = new Cart_item();
                    tmp.product = prDAO.GetProDuctByID(cart[0].ProductID);
                    tmp.ProductID = cart[0].ProductID;
                    tmp.Quantity = cart[0].Quantity;
                    tmp.Price = (int)prDAO.GetProDuctByID(cart[0].ProductID).PriceNew;
                    var list = new List<Cart_item>();
                    list.Add(tmp);
                    Session[SessionCart] = list;
                    if (Session["id"] != null)
                    {
                         Cart icart = new Cart();
                         icart.ProductID = tmp.ProductID;
                         icart.Quantity = tmp.Quantity;
                         icart.CustomerID = (int)Session["id"];
                         cartDAO.InsertCart(icart);
                    }
                    return Json(new
                    {
                         status = true,
                         Count = list.Count(),
                         productid = tmp.ProductID,
                         image = tmp.product.Image,
                         name = tmp.product.ProductName,
                         quantity = tmp.Quantity,
                         price = ((int)tmp.product.PriceNew).ToString("N0"),
                         subtotal = ((int)tmp.product.PriceNew).ToString("N0"),
                         method = "add"
                    });
               }

          }
          public JsonResult Delete(int id)
          {
               var sessionCart = (List<Cart_item>)Session[SessionCart];//lấy danh sách các sản phẩm trong giỏ hàng hiện có
               sessionCart.RemoveAll(x => x.ProductID == id);
               if (Session["id"] != null)
               {
                    cartDAO.DeleteCart(cartDAO.GetCart(id, (int)(Session["id"])));// xóa giỏ hàng trong bảng giỏ hàng của user
               }
               Session[SessionCart] = sessionCart;
               var list = (List<Cart_item>)sessionCart;
               int priceTotal = 0;
               foreach (var item1 in list)
               {
                    int temp = ((int)item1.Price) * ((int)item1.Quantity);
                    priceTotal += temp;
               }
               return Json(new
               {

                    status = true,
                    productid = id,
                    subtotal = priceTotal.ToString("N0"),
                    Count = list.Count()
               });
          }
          public JsonResult Update(int id, int quantity)
          {
               var sessionCart = (List<Cart_item>)Session[SessionCart];//lấy danh sách các sản phẩm trong giỏ hàng hiện có
               int tempPrice = 0;
               if (sessionCart.Exists(x => x.ProductID == id))
               {
                    foreach (var item in sessionCart)
                    {
                         if (item.ProductID == id)
                         {
                              tempPrice = item.Price;
                              item.Quantity = quantity;
                              if (Session["id"] != null)
                              {
                                   Cart icart = new Cart();
                                   icart.ProductID = item.ProductID;
                                   icart.Quantity = item.Quantity;
                                   icart.CustomerID = (int)Session["id"];
                                   cartDAO.UpdateCart(icart);
                              }
                         }

                    }
               }
               Session[SessionCart] = sessionCart;
               var list = (List<Cart_item>)sessionCart;
               int priceTotal = 0;
               foreach (var item1 in list)
               {
                    int temp = ((int)item1.Price) * ((int)item1.Quantity);
                    priceTotal += temp;
               }
               return Json(new
               {

                    status = true,
                    productid = id,
                    quantity = quantity,
                    tempprice = (tempPrice * quantity).ToString("N0"),
                    subtotal = priceTotal.ToString("N0"),
                    Count = list.Count()
               });
          }


     }
}