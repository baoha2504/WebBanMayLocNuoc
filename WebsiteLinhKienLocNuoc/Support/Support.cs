using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
namespace WebsiteLinhKienLocNuoc.Support
{
     public class Support
     {
          public string EncodePassword(string originalPassword)
          {
               //Declarations
               Byte[] originalBytes;
               Byte[] encodedBytes; 
               MD5 md5;

               //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
               md5 = new MD5CryptoServiceProvider();
               originalBytes = System.Text.ASCIIEncoding.Default.GetBytes(originalPassword);
               encodedBytes = md5.ComputeHash(originalBytes);

               //Convert encoded bytes back to a 'readable' string
               return BitConverter.ToString(encodedBytes);
          }
          public string GetFileExtension(string s)
          {
               string[] Name_extension = s.Split('.');
               int countarray = Name_extension.Count();
               s = Name_extension[countarray - 1];
               return s;
          }
     }
}