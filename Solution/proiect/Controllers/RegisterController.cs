using proiect.BusinessLogic;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class RegisterController : Controller
    {
          private readonly ISession _session;
          // GET: Register
          public RegisterController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }
          // GET : Login
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult LogIn(UserLogin data)
          {
               if (ModelState.IsValid)
               {
                    ULoginData uData = new ULoginData
                    {
                         Credential = data.Credential,
                         Password = data.Password,
                         LoginIp = data.LoginIp,
                         LoginDateTime = DateTime.Now,
                    };
                    ULoginResp resp = _session.UserLoginAction(uData);
                    if (resp.Status)
                    {
                         //ADD COOKIE

                         return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                         ModelState.AddModelError("", resp.ActionStatusMsg);
                         return View();
                    }
               }
               return View();
          }


          public ActionResult SignIn()
          {
               return View();
          }

          public ActionResult LogIn()
          {
               return View();
          }
     }
}