using AutoMapper;
using Microsoft.Ajax.Utilities;
using proiect.BusinessLogic;
using proiect.BusinessLogic.AppBL;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Domain.Enums;
using proiect.Helpers;
using proiect.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace proiect.Controllers
{
     public class LoginController : Controller
     {
          private readonly ISession _session;
          
          // GET: Register
          public LoginController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }
          public ActionResult LogIn()
          {
               return View();
          }

          // GET : Login
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult LogIn(UserLogin data)
          {
               if (ModelState.IsValid)
               {
                    var dataUser = new ULoginData
                    {
                         Credential = data.Credential,
                         Password = data.Password, // Do not hash here; the service will take care of it.
                         LoginIp = Request.UserHostAddress,
                         LoginDateTime = DateTime.Now,
                    };

                    ULoginResp resp = _session.UserLoginAction(dataUser);
                    if (resp.Status)
                    {
                         FormsAuthentication.SetAuthCookie(dataUser.Credential, false);
                         Session["UserName"] = data.Credential;
                         return RedirectToAction(resp.Message == "Admin" ? "IndexAdmin" : "Index", "Home");
                    }
                    else
                    {
                         ModelState.AddModelError("", resp.Message);
                         return View(data);
                    }
               }
               return View(data);
          }




     }
}