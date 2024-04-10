using proiect.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proiect.BusinessLogic;
using proiect.Extensions;
using System.Web.Routing;
using proiect.Domain.Enums;

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
          /*
          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
               if (apiCookie != null) 
               {
                    var profile = _sessionBusinessLogic.GetUserByCookie(apiCookie.Value);
                    if (profile != null && profile.Level != URole.Admin) 
                    {
                         HttpContext.Current.SetMySessionObject(profile);
                    }
                    else
                    {
                         filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Error404" }));
                    }
               }
          }
          */
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

               if (profile.Level != URole.Admin)
               {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { controller = "Access", action = "Denied" }));
               }
               else
               {
                    HttpContext.Current.SetMySessionObject(profile);
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