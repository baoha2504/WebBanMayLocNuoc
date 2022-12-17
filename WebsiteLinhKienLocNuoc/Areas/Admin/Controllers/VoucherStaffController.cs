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
    public class VoucherStaffController : Controller
    {
        private Voucher_DAO vcDAO = new Voucher_DAO();
        private Customer_DAO cusDAO = new Customer_DAO();
        // GET: Admin/Voucher
        public ActionResult Index()
        {
            List<Voucher> vouchers = vcDAO.GetVoucher();
            for (int i = 0; i < vouchers.Count; i++)
            {
                vouchers[i].Customer = cusDAO.GetCustomerbyID(vouchers[i].CustomerID);
            }
            ViewBag.Customers = cusDAO.GetCustomer();
            return View(vouchers);
        }

        public ActionResult Add(string VoucherCode, string CustomerID, string SalePercent, string MaximumDis, string MiximunVal)
        {
            Voucher voucher = new Voucher();
            voucher.VoucherCode = VoucherCode;
            voucher.CustomerID = Int32.Parse(CustomerID);
            voucher.SalePercent = Int32.Parse(SalePercent);
            voucher.MaximumDis = Int32.Parse(MaximumDis);
            voucher.MiximunVal = Int32.Parse(MiximunVal);
            vcDAO.InsertVoucher(voucher);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string VoucherID, string VoucherCode, string CustomerID, string SalePercent, string MaximumDis, string MiximunVal)
        {
            Voucher voucher = new Voucher();
            voucher.VoucherID = Int32.Parse(VoucherID);
            voucher.VoucherCode = VoucherCode;
            voucher.CustomerID = Int32.Parse(CustomerID);
            voucher.SalePercent = Int32.Parse(SalePercent);
            voucher.MaximumDis = Int32.Parse(MaximumDis);
            voucher.MiximunVal = Int32.Parse(MiximunVal);
            vcDAO.UpdateVoucher(voucher);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string VoucherID)
        {
            Voucher voucher = new Voucher();
            voucher.VoucherID = Int32.Parse(VoucherID);
            //vcDAO.UpdateVoucher(voucher);
            return RedirectToAction("Index");
        }
    }
}