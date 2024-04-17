using proiect.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class LoginUserController : Controller
    {
          // GET: LoginUser
          [DoctorMod]
          public ActionResult SearchDonator()
          {
            return View();
          }
          public ActionResult UserPage()
          {
               return View();
          }

     }
}