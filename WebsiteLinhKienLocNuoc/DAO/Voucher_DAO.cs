using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebsiteLinhKienLocNuoc.Models;

namespace WebsiteLinhKienLocNuoc.DAO
{
    public class Voucher_DAO
    {
        private Connection cn = new Connection();
        public List<Voucher> GetVoucher()
        {
            List<Voucher> lst = cn.ConvertToList<Voucher>(GetDataVoucher());
            return lst;
        }
        public DataTable GetDataVoucher()
        {
            string query = "select * from Voucher Order By VoucherID DESC";
            DataTable tb = cn.LoadTable(query);
            return tb;
        }

        public Voucher GetVoucherByCode(string code)
        {
            string query = "select * from Voucher Where VoucherCode = @code";
            string[] para = new string[1] { "@code" };
            object[] value = new object[1] { code };
            DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
            Voucher sp = cn.ConvertToList<Voucher>(tb).FirstOrDefault();
            return sp;
        }

        public Voucher GetVoucherByID(int? id)
        {
            string query = "select * from Voucher Where VoucherID = @id";
            string[] para = new string[1] { "@id" };
            object[] value = new object[1] { id };
            DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
            Voucher sp = cn.ConvertToList<Voucher>(tb).FirstOrDefault();
            return sp;
        }

        public int InsertVoucher(Voucher voucher)
        {
            string query = "insertVoucher";
            string[] para = new string[5] { "@vouchercode", "@customerid", "@salepercent", "@maximumdis", "@miximunval" };
            object[] value = new object[5] { voucher.VoucherCode, voucher.CustomerID, voucher.SalePercent, voucher.MaximumDis, voucher.MiximunVal };
            return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }

        public int UpdateVoucher(Voucher voucher)
        {
            string query = "updateVoucher";
            string[] para = new string[6] { "@voucherid", "@vouchercode", "@customerid", "@salepercent", "@maximumdis", "@miximunval" };
            object[] value = new object[6] { voucher.VoucherID, voucher.VoucherCode, voucher.CustomerID, voucher.SalePercent, voucher.MaximumDis, voucher.MiximunVal };
            return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }
    }
}