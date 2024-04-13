using proiect.Domain.Entities.User;
using proiect.Domain.Enums;
using proiect.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace proiect.VerifyRole
{
     public class IsAdmin 
     {
          public static bool IsUserAdmin()
          {
               if (HttpContext.Current.User.Identity.IsAuthenticated)
               {
                    return true;
               }
               else
               { return false; }
               
          }
          

     }
}
