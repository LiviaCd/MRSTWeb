using Antlr.Runtime.Tree;
using proiect.BusinessLogic;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
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
     public class HomeController : Controller
     {
          // GET: Home
          public ActionResult Index()
          {
               UserData u = new UserData();
               u.UserName = "Customer";

               return View(u);
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
               UserData u = new UserData();
               u.UserName = "Customer";
               return View(u);
          }
          public ActionResult Contact()
          {
               UserData u = new UserData();
               u.UserName = "Customer";
               u.BloodType = new List<BloodTypeModel>
              {
                  new BloodTypeModel { Name = "Grupa A, Rh(+)", ImageUrl = "/assets3/images/BloodType/typeA.png" },
                  new BloodTypeModel { Name = "Grupa B, Rh(+)", ImageUrl = "/assets3/images/BloodType/typeB.png" },
                  new BloodTypeModel { Name = "Grupa AB, Rh(+)", ImageUrl = "/assets3/images/BloodType/typeAB.png" },
                  new BloodTypeModel { Name = "Grupa O, Rh(+)", ImageUrl = "/assets3/images/BloodType/typeO.png" },
                  new BloodTypeModel { Name = "Grupa A, Rh(-)", ImageUrl = "/assets3/images/BloodType/typeA.png" },
                  new BloodTypeModel { Name = "Grupa B, Rh(-)", ImageUrl = "/assets3/images/BloodType/typeB.png" },
                  new BloodTypeModel { Name = "Grupa AB, Rh(-)", ImageUrl = "/assets3/images/BloodType/typeAB.png" },
                  new BloodTypeModel { Name = "Grupa O, Rh(-)", ImageUrl = "/assets3/images/BloodType/typeO.png" },
              };
               return View(u);
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