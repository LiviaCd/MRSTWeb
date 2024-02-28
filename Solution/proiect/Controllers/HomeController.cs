using Antlr.Runtime.Tree;
using proiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

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

          public ActionResult SignIn()
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
                  new BloodTypeModel { Name = "Grupa 0, Rh(+)", ImageUrl = "/assets3/images/1Pozitiv.jpg" },
                  new BloodTypeModel { Name = "Grupa A, Rh(+)", ImageUrl = "/assets3/images/2Pozitiv.jpg" },
                  new BloodTypeModel { Name = "Grupa B, Rh(+)", ImageUrl = "/assets3/images/3Pozitiv.jpg" },
                  new BloodTypeModel { Name = "Grupa AB, Rh(+)", ImageUrl = "/assets3/images/4Pozitiv.jpg" },
                  new BloodTypeModel { Name = "Grupa 0, Rh(-)", ImageUrl = "/assets3/images/1Negativ.jpg" },
                  new BloodTypeModel { Name = "Grupa A, Rh(-)", ImageUrl = "/assets3/images/2Negativ.jpg" },
                  new BloodTypeModel { Name = "Grupa B, Rh(-)", ImageUrl = "/assets3/images/3Negativ.jpg" },
                  new BloodTypeModel { Name = "Grupa AB, Rh(-)", ImageUrl = "/assets3/images/4Negativ.jpg" }
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