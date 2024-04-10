using AutoMapper;
using Microsoft.Ajax.Utilities;
using proiect.BusinessLogic;
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
          public ActionResult LogOut()
          {
               Session.Abandon();
               FormsAuthentication.SignOut();
               if (Response.Cookies["X-KEY"] != null)
               {
                    var cookie = new HttpCookie("X-KEY")
                    {
                         Expires = DateTime.Now.AddDays(-1),
                         HttpOnly = true 
                    };
                    Response.Cookies.Add(cookie);
               }
               return RedirectToAction("LogIn", "Login");
          }



          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult LogIn(UserLogin data)
          {
               if (ModelState.IsValid)
               {
                    var dataUser = Mapper.Map<ULoginData>(data);

                    dataUser.LoginIp = Request.UserHostAddress;
                    dataUser.LoginDateTime = DateTime.Now;

                    ULoginResp resp = _session.UserLoginAction(dataUser);
                    if (resp.Status)
                    {
                         HttpCookie cookie = _session.GenCookie(data.Credential);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                         Session["UserName"] = data.Credential;
                         if (resp.Message == "Admin")
                              return RedirectToAction("IndexAdmin", "Admin");
                         return RedirectToAction("Index", "Home");
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