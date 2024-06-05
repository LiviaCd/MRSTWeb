using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect.Models.Profile
{
     public class MProfile
     {
          public string Email { get; set; }
          public string PhotoPath { get; set; }
          public HttpPostedFileBase PhotoFile { get; set; }
     }
}