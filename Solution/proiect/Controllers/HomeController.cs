using Antlr.Runtime.Tree;
using proiect.Attributes;
using proiect.BusinessLogic;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Extensions;
using proiect.Models;
using proiect.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace proiect.Controllers
{
     public class HomeController : BaseController
     {
          
          // GET: Home
          public ActionResult Index()
          {
               SessionStatus();
               if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
               {
                    return View (); 
               }
               return View();
              // return RedirectToAction("Index", "Home");
          }
          
          public ActionResult About()
          {
               SessionStatus();
               return View();
          }
          public ActionResult News()
          {
               SessionStatus();
               return View();
          }
          public ActionResult Contact()
          {
               SessionStatus();
               return View();
          }
          
          public ActionResult ErrorAccessDenied()
          {
               return View();
          }
          public ActionResult BloodType()
          {
               var type = Request.QueryString["t"];

               UserData u = new UserData();
               u.UserName = "Customer";
               u.SingleType = type;

               return View(u);
          }
          [HttpPost]
          public ActionResult BloodType(string btn) 
          {
               return RedirectToAction("BloodType", "Home", new {@t = btn}); 
          }
     }
}