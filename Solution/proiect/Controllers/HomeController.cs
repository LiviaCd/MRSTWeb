using Antlr.Runtime.Tree;
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
                    return RedirectToAction("LogIn", "Login");
               }
               var user = System.Web.HttpContext.Current.GetMySessionObject();
               UserData u = new UserData
               {
                   UserName = user.Username
               };
               return View(u);
          }
          
          public ActionResult IndexAdmin()
          {
               SessionStatus();
              // if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
              // {
               //     return RedirectToAction("LogIn", "Login");
              // }
               var user = System.Web.HttpContext.Current.GetMySessionObject();
               UserData u = new UserData
               {
                    UserName = user.Username
               };
               return View(u);
          }
          public ActionResult Users()
          {
               return View();
          }
          public ActionResult UserPage()
          {
               return View();
          }

          public ActionResult LogIn()
          {
               return View();
          }

          public ActionResult About()
          {
               return View();
          }
          public ActionResult News()
          {
               return View();
          }
          public ActionResult Contact()
          { 
               return View();
          }
          public ActionResult Pretest()
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