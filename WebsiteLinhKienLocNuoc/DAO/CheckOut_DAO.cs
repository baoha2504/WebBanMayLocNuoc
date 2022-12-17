using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteLinhKienLocNuoc.Models;
using System.Data;

namespace WebsiteLinhKienLocNuoc.DAO
{
     public class CheckOut_DAO
     {
          private Connection cn = new Connection();
          public int InsertOrder(Order order)
          {
               string query = "insertOrder";
               string[] para = new string[10] { "@customerid", "@paymentid", "@shippingid", "@shippingadd", "@shippingfee", "@discount", "@total","@note","@name","@phone" };
               object[] value = new object[10] { order.CustomerID,order.PaymentID,order.ShippingID,order.ShippingAddress,order.ShippingFee,order.Discount,order.Total,order.Note,order.Name, order.Phone };
               return cn.Excute_SqlInserted(query, CommandType.StoredProcedure, para, value);
          }
          public int InsertOrderHistory(OrderStatusHistory orderstatusHistory)
          {
               string query = "insertOrderStatusHistory"; 
               string[] para = new string[3] {"@statusname","@orderid" ,"@cancelby" };
               object[] value = new object[3] { orderstatusHistory.OrderStatusName,orderstatusHistory.OrderID,orderstatusHistory.CanceledBy};
               return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
          public int InsertOrderDetail(OrderDetail orderDetail)
          {
               string query = "insertOrderDetail";
               string[] para = new string[4] { "@orderid" , "@productid" , "@quantity" , "@price"};
               object[] value = new object[4] { orderDetail.OrderID,orderDetail.ProductID,orderDetail.Quantity,orderDetail.Price };
               return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
     }
}