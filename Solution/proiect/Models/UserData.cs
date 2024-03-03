using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect.Models
{
     public class BloodTypeModel
     {
          public string Name { get; set; }
          public string ImageUrl { get; set; } 
     }
     public class UserData
     {
          public string UserName { get; set; }
          public string SingleType { get; set; }
          public List<BloodTypeModel> BloodType { get; set; }
     }
}