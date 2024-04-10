using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult IndexAdmin()
        {
            return View();
        }
          public ActionResult ContactAdmin()
          {
               return View();
          }

          public ActionResult AboutAdmin()
          {
               return View();
          }
          public ActionResult NewsAdmin()
          {
               return View();
          }

          public ActionResult PretestAdmin()
          {
               return View();
          }

          public ActionResult UsersAdmin()
          {
               return View();
          }
     }
}