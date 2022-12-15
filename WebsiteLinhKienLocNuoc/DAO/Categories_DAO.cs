using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteLinhKienLocNuoc.Models;
using System.Data;


namespace WebsiteLinhKienLocNuoc.DAO
{
     public class Categories_DAO
     {
          private Connection cn = new Connection();
          public List<Category> GetCategories()
          {
               List<Category> lst = cn.ConvertToList<Category>(GetDataCategories());
               return lst;
          }
          public DataTable GetDataCategories() 
          {
               string query = "select * from Categories";
               DataTable tb = cn.LoadTable(query);
               return tb;
          }
          public Category GetNameCategories(int? id)
          {
               string query = "select CategoriesName from Categories where CategoriesID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               Category sp = cn.ConvertToList<Category>(tb).FirstOrDefault();
               return sp;
          }
          public Category GetCategoriesByid(int? id)
          {
               string query = "select * from Categories where CategoriesID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               Category sp = cn.ConvertToList<Category>(tb).FirstOrDefault();
               return sp;
          }
          public int InsertCategories(Category category)
          {
               string query = "insertCategory";
               string[] para = new string[1] { "@categoriesname" };
               object[] value = new object[1] { category.CategoriesName };
               return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
          public int UpdateCategories(Category category)
          {
               string query = "updateCategory";
               string[] para = new string[2] { "@categoriesid","@categoriesname" };
               object[] value = new object[2] { category.CategoriesID,category.CategoriesName };
               return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
     }
}