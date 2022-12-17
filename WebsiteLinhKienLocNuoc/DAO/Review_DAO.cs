﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteLinhKienLocNuoc.Models;
using System.Data;

namespace WebsiteLinhKienLocNuoc.DAO
{
     public class Review_DAO
     {
          private Connection cn = new Connection();
          public List<Review> GetReviewByCustomerid(int? id)
          {
               string query = "select * from Review where CustomerID = @id ORDER BY DateAdded DESC";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               List<Review> sp = cn.ConvertToList<Review>(tb);
               return sp; 
          }
          public List<Review> GetReviewByProductid(int? id)
          {
               string query = "select * from Review where ProductID = @id ORDER BY DateAdded DESC";
               string[] para = new string[1] { "@id" };
               object[] value = new object[1] { id };
               DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
               List<Review> sp = cn.ConvertToList<Review>(tb);
               return sp;
          }
          public void InsertReview(Review review)
          {
               string query = "insertReview";
               string[] para = new string[4] { "@productid",  "@customerid","numstar","comment" };
               object[] value = new object[4] { review.ProductID,review.CustomerID,review.NumStar,review.Comment};
               cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
          }
     }
}