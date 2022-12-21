using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebsiteLinhKienLocNuoc.Models;


namespace WebsiteLinhKienLocNuoc.DAO
{
    public class Customer_DAO
    {
        private Connection cn = new Connection();
        public List<Customer> GetCustomer()
        {
            List<Customer> lst = cn.ConvertToList<Customer>(GetDataCustomers());
            return lst;
        }
        public DataTable GetDataCustomers()
        {
            string query = "select * from Customer where Access < 3";
            DataTable tb = cn.LoadTable(query);
            return tb;
        }
        public Customer GetCustomerbyID(int? id)
        {
            string query = "select * from Customer where CustomerID = @id";
            string[] para = new string[1] { "@id" };
            object[] value = new object[1] { id };
            DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
            Customer cus = cn.ConvertToList<Customer>(tb).FirstOrDefault();
            return cus;
        }
        public Customer GetCustomerbyEmail(string email)
        {
            string query = "select * from Customer where Email = @email";
            string[] para = new string[1] { "@email" };
            object[] value = new object[1] { email };
            DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
            Customer cus = cn.ConvertToList<Customer>(tb).FirstOrDefault();
            return cus;
        }
        public Customer GetLoginCustomer(string email, string password)
        {
            string query = "select * from Customer where Email = @email and Password = @password";
            string[] para = new string[2] { "@email", "@password" };
            object[] value = new object[2] { email, password };
            DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
            Customer cus = cn.ConvertToList<Customer>(tb).FirstOrDefault();
            return cus;
        }
        public Customer GetLoginCustomer(int id, string password)
        {
            string query = "select * from Customer where CustomerID = @id and Password = @password";
            string[] para = new string[2] { "@id", "@password" };
            object[] value = new object[2] { id, password };
            DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
            Customer cus = cn.ConvertToList<Customer>(tb).FirstOrDefault();
            return cus;
        }
        public void InsertCustormer(Customer cus)
        {
            string query = "insertCustomer";
            string[] para = new string[11] { "@ho", "@ten", "@email", "@password", "@phone", "@company", "@address1", "@address2", "@city", "@access", "@prohibit" };
            object[] value = new object[11] { cus.FirstName, cus.LastName, cus.Email, cus.PassWord, cus.Phone, cus.Company, cus.Address1, cus.Address2, cus.City, cus.Access, cus.Prohibit };
            cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }
        public void UpdateCustormer(Customer cus)
        {
            string query = "updateCustomer";
            string[] para = new string[9] { "@id", "@ho", "@ten", "@email", "@phone", "@company", "@address1", "@address2", "@city" };
            object[] value = new object[9] { cus.CustomerID, cus.FirstName, cus.LastName, cus.Email, cus.Phone, cus.Company, cus.Address1, cus.Address2, cus.City };
            cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }
        public void UpdatePassCustormer(Customer cus)
        {
            string query = "updatePassCustomer";
            string[] para = new string[2] { "@id", "@password" };
            object[] value = new object[2] { cus.CustomerID, cus.PassWord };
            cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }
        public List<Customer> GetListCustomer()
        {
            string query = "select * from Customer";
            DataTable tb = cn.LoadTable(query);
            List<Customer> cus = cn.ConvertToList<Customer>(tb);
            return cus;
        }
        public List<Customer> GetListCustomer1()
        {
            string query = "select * from Customer where access =1 ";
            DataTable tb = cn.LoadTable(query);
            List<Customer> cus = cn.ConvertToList<Customer>(tb);
            return cus;
        }
        public List<Customer> GetListCustomer2()
        {
            string query = "select * from Customer where access =2 ";
            DataTable tb = cn.LoadTable(query);
            List<Customer> cus = cn.ConvertToList<Customer>(tb);
            return cus;
        }
        public List<Customer> GetListCustomer3()
        {
            string query = "select * from Customer where access =3 ";
            DataTable tb = cn.LoadTable(query);
            List<Customer> cus = cn.ConvertToList<Customer>(tb);
            return cus;
        }
        public int UpdateCustomerAccess(Customer customer)
        {
            string query = "updateCustomerAccessy";
            string[] para = new string[3] { "@customerid ", "@access", "@prohibit" };
            object[] value = new object[3] { customer.CustomerID, customer.Access, customer.Prohibit };
            return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }
        public int InsertCustomerAccess(Customer customer)
        {
            string query = "InsertCustomerAccessy";
            string[] para = new string[4] { "@email ", "password", "@access", "@prohibit" };
            object[] value = new object[4] { customer.Email, customer.PassWord, customer.Access, customer.Prohibit };
            return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }
    }
}