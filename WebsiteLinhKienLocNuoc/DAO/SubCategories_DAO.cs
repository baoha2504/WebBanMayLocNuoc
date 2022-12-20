using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteLinhKienLocNuoc.Models;
using System.Data;

namespace WebsiteLinhKienLocNuoc.DAO
{
     public class SubCategories_DAO
     {
          private Connection cn = new Connection();
          public List<SubCategory> GetSubCategories()
          {
               List<SubCategory> lst = cn.ConvertToList<SubCategory>(GetDataSubCategories());
               return lst;
          }
          public DataTable GetDataSubCategories()
          {
               string query = "select * from SubCategories";
               DataTable tb = cn.LoadTable(query);
               return tb; 
          }
          public SubCategory GetNameSubCategories(int id)
          {
               string query = "select SubCategoriesName from SubCategories where SubCategoriesID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               SubCategory sp = cn.ConvertToList<SubCategory>(tb).FirstOrDefault();
               return sp;
          }
          public SubCategory GetSubCategoriesByid(int? id)
          {
               string query = "select * from SubCategories where SubCategoriesID = @id";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               SubCategory sp = cn.ConvertToList<SubCategory>(tb).FirstOrDefault();
               return sp;
          }
          public int InsertSubCategories(SubCategory subcategory)
          {
               string query = "insertSubCategory";
               string[] para = new string[2] { "@subcategoriesname", "@categoriesid" };
               object[] value = new object[2] { subcategory.SubCategoriesName, subcategory.CategoriesID };
               return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
          public int UpdateSubCategories(SubCategory subcategory)
          {
               string query = "updateSubCategory";
               string[] para = new string[3] { "@subcategoriesname", "@categoriesid", "@subcategoriesid" };
               object[] value = new object[3] { subcategory.SubCategoriesName, subcategory.CategoriesID, subcategory.SubCategoriesID };
               return cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
     }
}