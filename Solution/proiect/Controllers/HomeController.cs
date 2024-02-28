using Antlr.Runtime.Tree;
using proiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
               UserData u = new UserData();
               u.UserName = "Customer";
               u.BloodType = new List<string> { "Grupa 0", "Grupa A", "Grupa B", "Grupa AB" };
               return View(u);
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
               u.BloodType = new List<string> { "Grupa 0", "Grupa A", "Grupa B", "Grupa AB" };
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