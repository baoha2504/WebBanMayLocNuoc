using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebsiteLinhKienLocNuoc.Models;


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
            OrderStatusHistory sp = cn.ConvertToList<OrderStatusHistory>(tb).FirstOrDefault(); ;
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

    
        public string GetRevenueDay()
        {
            string query = "Select sum(total) as tol " +
                "from dbo.[Order], OrderStatusHistory " +
                "where dbo.[Order].OrderID = OrderStatusHistory.OrderID " +
                "and OrderStatusHistory.OrderStatusName = N'Đã Giao Hàng' " +
                "and Cast(OrderStatusHistory.DateAdd as Date)= cast(GETDATE() as date)";
            DataTable tb = cn.LoadTable(query);
            string lst = tb.Rows[0][0].ToString();
            return lst;
        }
        public string GetRevenueMonth()
        {
            string query = "Select sum(total) " +
                "from dbo.[Order], OrderStatusHistory " +
                "where dbo.[Order].OrderID = OrderStatusHistory.OrderID " +
                "and OrderStatusHistory.OrderStatusName = N'Đã Giao Hàng' " +
                "and MONTH(OrderStatusHistory.DateAdd) = MONTH(GETDATE())";
            DataTable tb = cn.LoadTable(query);
            string lst = tb.Rows[0][0].ToString();
            return lst;
        }
        public string GetRevenueYear()
        {
            string query = "Select sum(total) " +
                "from dbo.[Order], OrderStatusHistory " +
                "where dbo.[Order].OrderID = OrderStatusHistory.OrderID " +
                "and OrderStatusHistory.OrderStatusName = N'Đã Giao Hàng' " +
                "and YEAR(OrderStatusHistory.DateAdd) = YEAR(GETDATE())";
            DataTable tb = cn.LoadTable(query);
            string lst = tb.Rows[0][0].ToString();
            return lst;
        }
        public string GetCountOderInWeek()
        {
            string query = "SELECT Count(*) from dbo.[Order] " +
                "Where DateAdd >= (select dateadd(week, datediff(week, 0, getdate()), 0) as S) " +
                "and DateAdd < (select dateadd(week, datediff(week, 0, getdate()), 0) + 7 as F)";
            DataTable tb = cn.LoadTable(query);
            string lst = tb.Rows[0][0].ToString();
            return lst;
        }
        public string GetCountOderInLastWeek()
        {
            string query = "SELECT Count(*) from dbo.[Order] " +
                "Where DateAdd >= (select dateadd(week, datediff(week, 0, getdate()), 0)-7 as S) " +
                "and DateAdd < (select dateadd(week, datediff(week, 0, getdate()), 0) as F)";
            DataTable tb = cn.LoadTable(query);
            string lst = tb.Rows[0][0].ToString();
            return lst;
        }
        public string GetTotalMoneyOfMachineInMonth()
        {
            string query = "select sum(total) " +
                "from dbo.[Order] join OrderStatusHistory on dbo.[Order].OrderID = OrderStatusHistory.OrderID " +
                "join OrderDetail on OrderStatusHistory.OrderID = OrderDetail.OrderID " +
                "join Product on Product.ProductID = OrderDetail.ProductID " +
                "where OrderStatusHistory.OrderStatusName = N'Đã Giao Hàng' " +
                "and MONTH(OrderStatusHistory.DateAdd) = MONTH(GETDATE()) " +
                "and ProductName like N'%Máy lọc nước%'";
            DataTable tb = cn.LoadTable(query);
            string lst = tb.Rows[0][0].ToString();
            return lst;
        }
    }
}