using System.Web.Mvc;
using WebsiteLinhKienLocNuoc.DAO;
using WebsiteLinhKienLocNuoc.Models;

namespace WebsiteLinhKienLocNuoc.Areas.Admin.Controllers
{
    public class AccountAdminController : Controller
    {
        private Customer_DAO cusDAO = new Customer_DAO();
        WebsiteLinhKienLocNuoc.Support.Support sp = new WebsiteLinhKienLocNuoc.Support.Support();
        // GET: Admin/AccountAdmin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string useremail, string userpassword)
        {
            userpassword = sp.EncodePassword(userpassword);
            Customer customer = cusDAO.GetLoginCustomer(useremail, userpassword);
            if (customer != null && customer.Prohibit == 1)
            {
                if (customer.Access == 1)
                {
                    Session["adminid"] = customer.CustomerID;
                    Session["username"] = customer.FirstName + customer.LastName;
                    return RedirectToAction("Index", "Dashboard");
                } else if (customer.Access == 2)
                {
                    Session["adminid"] = customer.CustomerID;
                    Session["username"] = customer.FirstName + customer.LastName;
                    return RedirectToAction("Index", "DashboardStaff");
                }
                else
                {
                    TempData["Error"] = "Không phải tài khoản Admin";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["adminid"] = "";
            Session["username"] = "";
            return RedirectToAction("Login");

        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string userpassword, string newpassword)
        {
            userpassword = sp.EncodePassword(userpassword);

            Customer customer = cusDAO.GetLoginCustomer((int)Session["adminid"], userpassword);

            if (customer != null)
            {
                customer.PassWord = sp.EncodePassword(newpassword);
                cusDAO.UpdatePassCustormer(customer);
                ViewBag.alert = "Đổi Mật Khẩu Thành Công";
            }
            else
            {
                ViewBag.danger = "Mật Khẩu Không Đúng";
            }
            return View();
        }
    }
}