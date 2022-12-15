using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteLinhKienLocNuoc.Models;
using System.Data;


namespace WebsiteLinhKienLocNuoc.DAO
{
     public class Cart_DAO
     {
          private Connection cn = new Connection();
          public List<Cart> GetCartByCustomerID(int id)
          {
               string query = "SELECT * FROM Cart Where  CustomerID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value); 
               List<Cart> sp = cn.ConvertToList<Cart>(tb);
               return sp;
          }
          public void UpdateCart(Cart cart)
          {
               string query = "updateCart";
               string[] para = new string[3] { "@productid", "@quantity", "@customerid"};
               object[] value = new object[3] { cart.ProductID,cart.Quantity,cart.CustomerID};
               cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
          public void InsertCart(Cart cart)
          {
               string query = "insertCart";
               string[] para = new string[3] { "@productid", "@quantity", "@customerid" };
               object[] value = new object[3] { cart.ProductID, cart.Quantity, cart.CustomerID };
               cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
          public void DeleteCart(Cart cart)
          {
               string query = "deleteCart";
               string[] para = new string[3] { "@productid", "@quantity", "@customerid" };
               object[] value = new object[3] { cart.ProductID, cart.Quantity, cart.CustomerID };
               cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
          public Cart GetCart(int? productid,int? customerid )
          {
               string query = "select * from Cart Where ProductID = @productid AND CustomerID= @customerid";
               string[] para = new string[2] { "@productid", "@customerid" };
               object[] value = new object[2] { productid , customerid };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               Cart sp = cn.ConvertToList<Cart>(tb).FirstOrDefault();
               return sp;
          }
     }
}