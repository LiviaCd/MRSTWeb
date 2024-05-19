using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Enums;
using proiect.Extensions;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace proiect.Attributes
{
     public class AdminModAttribute : ActionFilterAttribute
     {
          private readonly ISession _sessionBusinessLogic;

          public AdminModAttribute()
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

               if (profile.Level == URole.Admin && profile.BlockTime < DateTime.Now)
               {
                    HttpContext.Current.SetMySessionObject(profile);
               }
               else
               {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                             new { controller = "LoginUser", action = "AccountBlock" }));

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