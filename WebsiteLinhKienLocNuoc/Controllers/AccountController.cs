﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using WebsiteLinhKienLocNuoc.Models;
using WebsiteLinhKienLocNuoc.DAO;
using WebsiteLinhKienLocNuoc.Support;
using WebsiteLinhKienLocNuoc.Library;


namespace WebsiteLinhKienLocNuoc.Controllers
{
     public class AccountController : Controller
     {
          // GET: Account 
          private Customer_DAO cusDAO = new Customer_DAO();
          private Cart_DAO cartDAO = new Cart_DAO();
          private Product_DAO prDAO = new Product_DAO();
          private Review_DAO reDAO = new Review_DAO();
          WebsiteLinhKienLocNuoc.Support.Support sp = new WebsiteLinhKienLocNuoc.Support.Support();

          public ActionResult Register()
          {

               return View();
          }
          [HttpPost]
          public ActionResult confirmRegister(string firstname, string lastname, string email, string telephone, string company, string address_1, string address_2, string city, string password, string confirm)
          {
                Customer cus = new Customer();
                cus.FirstName = firstname;
                cus.LastName = lastname;
                cus.Email = email;
                cus.Phone = telephone;
                cus.Company = company;
                cus.Address1 = address_1;
                cus.Address2 = address_2;
                cus.City = city;
                cus.PassWord = sp.EncodePassword(password);
                cus.Access = 3;
                cus.Prohibit = 1;
            if (password != confirm)
               {
                    TempData["Error"] = "Mật khẩu nhập lại không khớp";
                    Message.set_flash("Mật khẩu nhập lại không khớp", "error");
                    
               }
               else if (!CheckExistEmail(email))
               {

                    TempData["Error"] = "Địa chỉ Email đã được sử dụng";
                    Message.set_flash("Địa chỉ Email đã được sử dụng", "error");
                   
               }
               else
               {
                    
                   
                    if (InsertCustomer(cus) == true)
                    {
                         TempData["Succes"] = "Thành Công";
                         Message.set_flash("Đăng ký thành công", "success");
                    }
                    else
                    {
                         TempData["Error"] = "Thất Bại";
                         Message.set_flash("Thất Bại", "error");
                    }
               }
               return RedirectToAction("Register");
          }

          public ActionResult Login()
          {
               //System.Web.HttpContext.Current.Session["Message"] = "";
               return View();
          }
          public ActionResult Logout()
          {

               Session["id"] = null;
               Session["user"] = null;
               Session["SessionCart"] = null;
               return Redirect("~/");
          }
          public ActionResult ForgotPassword()
          {

               return View();
          }
          [HttpPost]
          public ActionResult CheckAccount(string email)
          {

               if (CheckExistEmail(email))
               {

                    TempData["Error"] = "Không tìm thấy tài khoản nào khớp email";
                    Message.set_flash("Không tìm thấy tài khoản nào khớp email", "error");
                    return RedirectToAction("ForgotPassword");
               }
               else
               {
                    Customer customer = cusDAO.GetCustomerbyEmail(email);
                    string hasCustomerid = sp.EncodePassword(customer.CustomerID.ToString());
                    sendMail(email);
                    Message.set_flash("Xác nhận thông tin cấp lại mật khẩu tại email", "success");
                    return RedirectToAction("ForgotPassword");
                    //return RedirectToAction("ConfirmForgotPassword", new { email = customer.Email,id= hasCustomerid });
               }
          }
          public void sendMail(string email)
          {

               Customer customer = cusDAO.GetCustomerbyEmail(email);
               string hasCustomerid = sp.EncodePassword(customer.CustomerID.ToString());
            // email gửi đi và email nhận
               MailMessage mm = new MailMessage(Mail.email, email);
               mm.Subject = "Cấp Lại Mật khẩu Linh Kiện Lọc Nước MTA";
               mm.Body = "Dear Mr." + customer.LastName + "," +
                   "Chúng tôi đã nhận được yêu cầu reset đổi mật khẩu của bạn, vui lòng tạo mới mật khẩu qua đường dẫn sau : https://linhkienlocnuocMTA.com/Account/ConfirmForgotPassword/" + hasCustomerid + "?email=" + email + "";
               mm.From = new MailAddress(Mail.email, "Linh kiện lọc nước MTA");
               mm.To.Add(new MailAddress(email));
               mm.BodyEncoding = System.Text.Encoding.UTF8;
               mm.IsBodyHtml = true;
               mm.Priority = MailPriority.High;
               SmtpClient smtp = new SmtpClient();
               smtp.Host = "stmp.gmail.com";
               smtp.Timeout = 10000;
               smtp.Port = 587;
               smtp.EnableSsl = true;
               smtp.UseDefaultCredentials = false;
            /// Email dùng để gửi đi
               NetworkCredential nc = new NetworkCredential(Mail.email, Mail.password);
               smtp.Credentials = nc;   
               smtp.EnableSsl = true;
               smtp.Send(mm); // ​​Google sẽ không còn hỗ trợ việc sử dụng những ứng dụng hoặc thiết bị của bên thứ ba 
               //ViewBag.mess = item.email;
        }
          public ActionResult ConfirmForgotPassword(string email, string id)
          {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
               ViewBag.email = email;
               ViewBag.id = id;
               return View();
          }
          [HttpPost]
          public JsonResult ConfirmFGnew(string password, string confirm, string email, string id)
          {
               if (password != confirm)
               {
                    Message.set_flash("Mật khẩu nhập lại không khớp", "error");
                    return Json(new
                    {
                         status = false
                    });
               }
               else
               {
                    Customer customer = cusDAO.GetCustomerbyEmail(email);
                    if (customer != null && sp.EncodePassword(customer.CustomerID.ToString()) == id)
                    {
                         customer.PassWord = sp.EncodePassword(password);
                         cusDAO.UpdatePassCustormer(customer);
                         Message.set_flash("Thay đổi mật khẩu thành công ", "success");
                         return Json(new
                         {
                              status = true
                         });
                    }
                    else
                    {
                         Message.set_flash("Đã có lỗi xảy ra", "error");
                         return Json(new
                         {
                              status = false
                         });
                    }

               }
          }


          [HttpPost]
          public ActionResult confirmLogin(string email, string password)
          {
               password = sp.EncodePassword(password);
               Customer cus = CheckCustomerLogin(email, password);
               if (cus == null)
               {

                    TempData["Error"] = "Sai tên tài khoản hoặc mật khẩu";
                    Message.set_flash("Sai tên tài khoản hoặc mật khẩu", "error");
                    return RedirectToAction("Login");
               }
               else
               {
                    if (cus.Prohibit == 2)
                    {

                         TempData["Error"] = "Tài khoản đã bị khóa";
                         Message.set_flash("Tài khoản đã bị khóa", "error");
                         return RedirectToAction("Login");
                    }
                    else
                    {
                         if (cus.Access == 3)
                         {

                              Session["id"] = cus.CustomerID;
                              Session["user"] = cus.LastName;
                              InsertToCart(cus.CustomerID);
                              UpdateSessionCart(cus.CustomerID);
                              Message.set_flash("Đăng Nhập Thành Công", "success");
                              return RedirectToAction("Index", "Home");
                         }
                         else
                         {
                              return RedirectToAction("Index", "Home");
                         }
                    }
               }
          }
          public Customer CheckCustomerLogin(string email, string password)
          {
               Customer cus = cusDAO.GetLoginCustomer(email, password);
               if (cus != null)
               {
                    return cus;
               }
               return null;
          }
          //Insert SessionCart to Cart Of Customer
          public void InsertToCart(int CustomerID)
          {
               List<Cart> listCart = cartDAO.GetCartByCustomerID(CustomerID);
               var sessionCart = Session["SessionCart"];
               var list = (List<Cart_item>)sessionCart;
               foreach (var item in listCart)
               {
                    if (list != null)
                    {
                         foreach (var item1 in list)
                         {
                              if (item.ProductID == item1.ProductID)
                              {
                                   item.Quantity += item1.Quantity;
                                   Cart cart = new Cart();
                                   cart.ProductID = item.ProductID;
                                   cart.Quantity = item.Quantity;
                                   cart.CustomerID = (int)Session["id"];
                                   cartDAO.UpdateCart(cart);
                                   list.Remove(item1);
                                   break;
                              }

                         }
                    }
               }
               if (list != null)
               {
                    foreach (var item1 in list)
                    {
                         Cart cart = new Cart();
                         cart.ProductID = item1.ProductID;
                         cart.Quantity = item1.Quantity;
                         cart.CustomerID = (int)Session["id"];
                         cartDAO.InsertCart(cart);
                    }
               }
          }
          public void UpdateSessionCart(int CustomerID)
          {
               Session["SessionCart"] = new List<Cart_item>();
               List<Cart> listCart = cartDAO.GetCartByCustomerID(CustomerID);
               var list = (List<Cart_item>)Session["SessionCart"];
               foreach (var item in listCart)
               {
                    var tmp = new Cart_item();
                    tmp.product = prDAO.GetProDuctByID(item.ProductID);
                    tmp.Price = (int)prDAO.GetProDuctByID(item.ProductID).PriceNew;
                    tmp.ProductID = (int)item.ProductID;
                    tmp.Quantity = (int)item.Quantity;
                    list.Add(tmp);
               }
               Session["SessionCart"] = list;
          }
          public ActionResult MyAccount()
          {
               if (Session["id"] != null)
               {
                    int customerid = (int)Session["id"];
                    Customer customer = cusDAO.GetCustomerbyID(customerid);
                    return View(customer);
               }
               else
               {
                    return RedirectToAction("Index", "Home");
               }
          }
          [HttpPost]
          public ActionResult updateInfor(string firstname, string lastname, string email, string phone, string company, string address_1, string address_2, string city)
          {
               Customer cus = new Customer();
               cus.CustomerID = (int)Session["id"];
               cus.FirstName = firstname;
               cus.LastName = lastname;
               cus.Email = email;
               cus.Phone = phone;
               cus.Company = company;
               cus.Address1 = address_1;
               cus.Address2 = address_2;
               cus.City = city;

               if (UpdateCustomer(cus) == true)
               {
                    Message.set_flash("Cập nhật thành công", "success");
               }
               else
               {
                    Message.set_flash("Cập nhật thất bại", "error");
               }
               return RedirectToAction("MyAccount");
          }
          public ActionResult ChangedPassword()
          {
               return View();
          }
          [HttpPost]
          public ActionResult changedPassword(string old_password, string new_password, string new_confirm)
          {
               int customerid = (int)Session["id"];
               Customer customer = cusDAO.GetCustomerbyID(customerid);
               if (sp.EncodePassword(old_password) != customer.PassWord)
               {
                    Message.set_flash("Mật khẩu cũ không chính xác", "error");
               }
               else if (new_password != new_confirm)
               {
                    Message.set_flash("Mật khẩu nhập lại không khớp", "error");
               }
               else
               {

                    Customer cus = new Customer();
                    cus.CustomerID = customer.CustomerID;
                    cus.PassWord = sp.EncodePassword(new_password);
                    if (ChangedPassCustomer(cus) == true)
                    {

                         Message.set_flash("Cập nhật thành công", "success");
                    }
                    else
                    {

                         Message.set_flash("Cập nhật thất bại", "error");
                    }
               }
               return RedirectToAction("ChangedPassWord");
          }
          public ActionResult MyComment()
          {
               if (Session["id"] != null)
               {
                    int customerid = (int)Session["id"];
                    List<Review> listRe = reDAO.GetReviewByCustomerid(customerid);
                    for (int i = 0; i < listRe.Count(); i++)
                    {
                         listRe[i].Customer = cusDAO.GetCustomerbyID(listRe[i].CustomerID);
                    }
                    return View(listRe);
               }
               else
               {
                    return RedirectToAction("Index", "Home");
               }
          }
          public bool CheckExistEmail(string email)
          {
               Customer cus = cusDAO.GetCustomerbyEmail(email);
               if (cus != null)
               {
                    return false;
               }
               return true;
          }
          public bool InsertCustomer(Customer cus)
          {
               try
               {
                    cusDAO.InsertCustormer(cus);
                    return true;
               }
               catch
               {
                    return false;
               }

          }
          public bool UpdateCustomer(Customer cus)
          {
               try
               {
                    cusDAO.UpdateCustormer(cus);
                    return true;
               }
               catch
               {
                    return false;
               }

          }
          public bool ChangedPassCustomer(Customer cus)
          {
               try
               {
                    cusDAO.UpdatePassCustormer(cus);
                    return true;
               }
               catch
               {
                    return false;
               }

          }
     }
}