using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteLinhKienLocNuoc.Models;
using System.Data;


namespace WebsiteLinhKienLocNuoc.DAO
{
     public class Order_DAO
     {
          private Connection cn = new Connection();
          public Order GetOrderByid(int? id)
          {
               string query = "select * from dbo.[Order] where OrderId = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               Order sp = cn.ConvertToList<Order>(tb).FirstOrDefault(); 
               return sp;
          }
          public List<OrderDetail> GetOrderDetailByOrderid(int? id)
          {
               string query = "SELECT * FROM dbo.OrderDetail WHERE OrderID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               List<OrderDetail> sp = cn.ConvertToList<OrderDetail>(tb);
               return sp;
          }
          public List<OrderStatusHistory> GetOrderStatusByOrderid(int? id)
          {
               string query = "SELECT * FROM dbo.OrderStatusHistory WHERE OrderID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               List<OrderStatusHistory> sp = cn.ConvertToList<OrderStatusHistory>(tb);
               return sp;
          }
          public List<Order> GetOrderByCustomerid(int? id)
          {
               string query = "select * from dbo.[Order] where CustomerID = @id ORDER BY DateAdd DESC";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               List<Order> sp = cn.ConvertToList<Order>(tb);
               return sp;
          }
          public OrderStatusHistory GetNewStatusByOrderid(int? id)
          {
               string query = "SELECT * FROM dbo.OrderStatusHistory WHERE OrderID = @id ORDER BY DateAdd DESC";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               OrderStatusHistory sp =  cn.ConvertToList<OrderStatusHistory>(tb).FirstOrDefault(); ;
               return sp;
          }
          public List<Order> GetOrderData()
          {
               string query = "select * from dbo.[Order] Order BY DateAdd DESC";
               DataTable tb = cn.LoadTable(query);
               List<Order> lst = cn.ConvertToList<Order>(tb);
               return lst;
          }
          public List<int> GetOrderDataApprove()
          {
               string query = "SELECT OrderID FROM dbo.OrderStatusHistory WHERE OrderStatusName=N'Đang Chờ Xác Nhận' ORDER BY DateAdd DESC";
               DataTable tb = cn.LoadTable(query);
               List<int> lst = cn.ConvertToList<int>(tb);
               return lst;
          }
     }
}