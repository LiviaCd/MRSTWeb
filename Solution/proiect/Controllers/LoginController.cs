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
                    Mapper.Initialize(cfg => cfg.CreateMap<UserLogin, ULoginData>());
                    var dataUser = Mapper.Map<ULoginData>(data);

                    dataUser.LoginIp = Request.UserHostAddress;
                    dataUser.LoginDateTime = DateTime.Now;

                    ULoginData uData = new ULoginData
                    {
                         Credential = data.Credential,
                         Password = data.Password,
                         LoginIp = Request.UserHostAddress,
                         LoginDateTime = DateTime.Now,
                    };
                    ULoginResp resp = _session.UserLoginAction(uData);
                    if (resp.Status)
                    {
                         HttpCookie cookie = _session.GenCookie(uData.Credential);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                         if (resp.Message == "Admin")
                             return RedirectToAction("IndexAdmin", "Home");

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

          

     }
}