using proiect.BusinessLogic;
using proiect.BusinessLogic.AppBL;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities;
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

        public RegisterController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }
        public ActionResult SignIn()
        {

            //var regData = new URegisterData
            //{
              //  UserName = "andrei",
               // Name = "andreii",
                //Password = "12345678986"
            //};

            //ULoginResp resp = _session.RegisterNewUserAction(regData);

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
                    Name = data.Name,
                    Password = data.Password,
                    ConfirmPassword = data.ConfirmPassword,
                    UserName = data.Credential,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now,
                
                };
                ULoginResp resp = _session.RegisterNewUserAction(uData);
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




    }
}