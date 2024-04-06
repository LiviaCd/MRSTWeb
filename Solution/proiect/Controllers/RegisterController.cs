using proiect.BusinessLogic;
using proiect.BusinessLogic.AppBL;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Helpers;
using proiect.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace proiect.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ISession _session;

        public RegisterController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }
        public ActionResult SignIn()
        {

            return View();
        }
        // GET : Register
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult SignIn(UserRegister data)

        {
            if (ModelState.IsValid)
            {
                URegisterData uData = new URegisterData
                {
                    Credential = data.Credential,
                    Password = data.Password,
                    ConfirmPassword = data.ConfirmPassword,
                    Email = data.Email,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now,
                
                };
                ULoginResp resp = _session.RegisterNewUserAction(uData);
                if (resp.Status)
                {
                         ULoginData user = new ULoginData
                         {
                              Credential = data.Credential,
                              Password = data.Password
                         };

                         _session.UserLoginAction(user);

                         HttpCookie cookie = _session.GenCookie(user.Credential);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);

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