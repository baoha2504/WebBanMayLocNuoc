using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteLinhKienLocNuoc.Models;
using System.Data;

namespace WebsiteLinhKienLocNuoc.DAO
{
     public class Payment_DAO
     {
          private Connection cn = new Connection();
          public Payment GetNamePayment(int? id)
          {
               string query = "select PaymentName from Payment where PaymentID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value); 
               Payment sp = cn.ConvertToList<Payment>(tb).FirstOrDefault();
               return sp;
          }
          public List<Payment> GetListPayment()
          {
               string query = "select * from Payment";
               DataTable tb = cn.LoadTable(query);
               List<Payment> cus = cn.ConvertToList<Payment>(tb);
               return cus;
          }
     }
}