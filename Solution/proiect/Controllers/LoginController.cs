using AutoMapper;
using Microsoft.Ajax.Utilities;
using proiect.BusinessLogic;
using proiect.BusinessLogic.DBModel.Seed;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Domain.Enums;
using proiect.Helpers;
using proiect.Models.Password;
using proiect.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace proiect.Controllers
{
     public class LoginController : BaseController
     {
          private readonly ISession _session;
        public string codeSend;
          
          // GET: Register
          public LoginController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }
          public ActionResult LogIn()
          {
               SessionStatus();
               return View();
          }
        public ActionResult ChangePassword()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserRegister user)
        {
            var _dbContext = new UserContext();
            if (ModelState.IsValid)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    var existingUser = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);

                    if (existingUser != null)
                    {
                        // Criptează noua parolă înainte de a o salva în baza de date
                        string encryptedPassword = LoginHelper.HashGen(user.Password);

                        // Actualizează parola criptată în baza de date
                        existingUser.Password = encryptedPassword;

                        _dbContext.SaveChanges();

                        return RedirectToAction("LogIn", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Utilizatorul nu a fost găsit.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Parola nouă și confirmarea parolei nu coincid.");
                }
            }

            return View(user);
        }
        public ActionResult ResetPassword()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(UserLogin user)
        {
            if (user.Email != null)
            {
                string email = user.Email;
                string codeSend = SendCode.SendCodeToUser(email); // Generate and send code to user's email
                TempData["ResetEmail"] = email; // Store the email in TempData to retrieve later
                TempData["CodeSend"] = codeSend; // Store the code in TempData to retrieve later
            }
            return RedirectToAction("ResetCode", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetCode(ModelResetPassword user)
        {
            string codeWrite = user.CodeWrite;
            UDBTable userLogin;
            string email = TempData["ResetEmail"] as string;
            string codeSend = TempData["CodeSend"] as string;
         
            if (codeWrite != null && codeWrite.Equals(codeSend))
            {
                // Codes match; proceed to reset password for the user
                // Redirect to a view where user can input new password
                using (var db = new UserContext())
                {
                    userLogin = db.Users.FirstOrDefault(us => us.Email == email);
                }
                if (userLogin == null)
                    Session["LoginStatus"] = "login";

                return RedirectToAction("ChangePassword", "Login", new { email = email });

            }
            else
            {
                // Codes don't match; redirect back to enter code again
                return RedirectToAction("ResetPassword", "Login");
            }
        }

        public ActionResult ResetCode()
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
               return RedirectToAction("Index", "Home");
          }



          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult LogIn(UserLogin data)
          {
               SessionStatus();
               // Store user profile in session
               HttpContext.Session["UserProfile"] = data;

               if (ModelState.IsValid)
               {
                    // Map user login data
                    var dataUser = Mapper.Map<ULoginData>(data);
                    dataUser.LoginIp = Request.UserHostAddress;
                    dataUser.LoginDateTime = DateTime.Now;

                    // Perform the user login action
                    ULoginResp resp = _session.UserLoginAction(dataUser);

                    if (resp.Status)
                    {
                         // Set authentication cookie
                         FormsAuthentication.SetAuthCookie(data.Email, false);

                         // Generate and add custom cookie
                         HttpCookie cookie = _session.GenCookie(data.Email);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                         // Store user name in session
                         Session["UserName"] = data.Email;

                         // Check block time
                         DateTime? blockTime = Session["BlockTime"] as DateTime?;
                         if (blockTime.HasValue && blockTime > DateTime.Now)
                         {
                              return RedirectToAction("AccountBlock", "LoginUser");
                         }
                         else
                         {
                              // Handle case where block time is still active
                              return RedirectToAction("UserPage", "LoginUser");
                         }
                    }
                    else
                    {
                         // Add error message to model state
                         ModelState.AddModelError("", resp.Message);
                    }
               }

               // Return the view with model state errors if any
               return View(data);
          }







     }
}