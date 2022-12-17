using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteLinhKienLocNuoc.Models;
using System.Data;

namespace WebsiteLinhKienLocNuoc.DAO
{
     public class Product_DAO
     {
          private Connection cn = new Connection();
          public List<Product> GetProduct()
          {
               List<Product> lst = cn.ConvertToList<Product>(GetDataProduct());
               return lst;
          }
          public DataTable GetDataProduct()
          {
               string query = "select * from Product Order By DateAdded DESC"; 
               DataTable tb = cn.LoadTable(query);
               return tb;
          }
          public Product GetProDuctByID(int? id)
          {
               string query = "select * from Product Where ProductID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               Product sp= cn.ConvertToList<Product>(tb).FirstOrDefault();
               return sp;
          }
          public List<Product> GetProDuctBySubCategory(int id)
          {
               string query = "select * from Product where Status = 1 and SubCategoriesID = @id ORDER BY DateAdded DESC ";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               List<Product> sp = cn.ConvertToList<Product>(tb);
               return sp;
          }
          public List<Product> GetProDuctByCategory(int id)
          {
               string query = "SELECT * FROM dbo.Product WHERE Status = 1 and SubCategoriesID in(SELECT SubCategoriesID FROM dbo.SubCategories WHERE CategoriesID = @id) ORDER BY DateAdded  DESC";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               List<Product> sp = cn.ConvertToList<Product>(tb);
               return sp;
          }
          public List<Product> GetFeatrueProDuct(int id)
          {
               string query = "select top(5) * from Product where SubCategoriesID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               List<Product> sp = cn.ConvertToList<Product>(tb);
               return sp;
          }
          public List<Product> GetNewProDuct()
          {
               string query = "SELECT TOP(9) * FROM dbo.Product where Status = 1 ORDER BY DateAdded DESC";
               DataTable tb = cn.LoadTable(query);
               List<Product> lst = cn.ConvertToList<Product>(tb);
               return lst;
          }
          public List<Product> FilterProDuct(string key)
          {
               string query = "SELECT * FROM dbo.Product where ProductName LIKE N'%'+@key+'%'";
               string[] para = new string[1] { "@key" };
               object[] value = new object[1] { key };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               List<Product> sp = cn.ConvertToList<Product>(tb);
               return sp;
          }
          public List<Product> GetRelateProDuct(int id)
          {
               string query = "select top(4) * from Product where SubCategoriesID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               List<Product> sp = cn.ConvertToList<Product>(tb);
               return sp;
          }
          public int InsertProduct(Product product)
          {
               string query = "insertProduct";
               string[] para = new string[11] { "@productname", "@categoriesid", "@summary", "@des", "@image", "@pricenew", "@priceold", "@brand", "@model", "@suppiler", "@status" };
               object[] value = new object[11] { product.ProductName, product.SubCategoriesID, product.Summary,product.Description,product.Image,product.PriceNew,product.PriceOld,product.Brand,product.Model,product.Suppiler,product.Status };
               return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
          public int UpdateProduct(Product product)
          {
               string query = "updateProduct";
               string[] para = new string[12] {"@productid", "@productname", "@categoriesid", "@summary", "@des", "@image", "@pricenew", "@priceold", "@brand", "@model", "@suppiler", "@status" };
               object[] value = new object[12] { product.ProductID,product.ProductName, product.SubCategoriesID, product.Summary, product.Description, product.Image, product.PriceNew, product.PriceOld, product.Brand, product.Model, product.Suppiler, product.Status };
               return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
          public int UpdateStatus(Product product)
          {
               string query = "updateStatus";
               string[] para = new string[2] { "@productid", "@status" };
               object[] value = new object[2] { product.ProductID, product.Status };
               return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
     }
}