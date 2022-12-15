using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteLinhKienLocNuoc.Models;
using System.Data;

namespace WebsiteLinhKienLocNuoc.DAO
{
     public class Shipping_DAO
     {
          private Connection cn = new Connection();
          public Shipping GetNameShipping(int? id)
          {
               string query = "select ShippingName from Shipping where ShippingID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               Shipping sp = cn.ConvertToList<Shipping>(tb).FirstOrDefault();
               return sp; 
          }
          public List<Shipping> GetListShipping()
          {
               string query = "select * from Shipping";
               DataTable tb = cn.LoadTable(query);
               List<Shipping> cus = cn.ConvertToList<Shipping>(tb);
               return cus;
          }
     }
}