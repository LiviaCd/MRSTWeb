using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Enums;
using proiect.Extensions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace proiect.Attributes
{
     public class LoginUserModAttribute : ActionFilterAttribute
     {
          private readonly ISession _sessionBusinessLogic;

          public LoginUserModAttribute()
          {
               var businessLogic = new BusinessLogic.BussinessLogic();
               _sessionBusinessLogic = businessLogic.GetSessionBL();
          }
          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
               if (apiCookie == null)
               {
                    RedirectToLogin(filterContext);
                    return;
               }

               var profile = _sessionBusinessLogic.GetUserByCookie(apiCookie.Value);
               if (profile == null)
               {
                    RedirectToLogin(filterContext);
                    return;
               }

               if (profile.Level == URole.User || profile.Level == URole.Doctor || profile.Level == URole.Admin)
               {
                    HttpContext.Current.SetMySessionObject(profile);
               }
               else
               {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { controller = "Home", action = "ErrorAccessDenied" }));
                   
               }
          }

          private void RedirectToLogin(ActionExecutingContext filterContext)
          {
               filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary(
                       new { controller = "Login", action = "LogIn" }));
          }
     }
}