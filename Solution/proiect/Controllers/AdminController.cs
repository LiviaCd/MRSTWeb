using proiect.Attributes;
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
          [AdminMod]
          public ActionResult Users()
        {
            return View();
        }
    }
}