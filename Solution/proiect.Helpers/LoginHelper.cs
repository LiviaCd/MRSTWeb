using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Helpers
{
     public class LoginHelper
     {
          public static string HashGen(string password)
          {
               MD5 md5 = new MD5CryptoServiceProvider();
               var originalBytes = Encoding.Default.GetBytes(password + "455GsfgGG98439");
               var encodedBytes = md5.ComputeHash(originalBytes);
               
               return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
          }
     }

}
