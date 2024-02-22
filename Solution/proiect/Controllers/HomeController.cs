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



     }
}