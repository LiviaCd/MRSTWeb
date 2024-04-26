using Microsoft.ApplicationInsights.Extensibility.Implementation;
using proiect.Attributes;
using proiect.BusinessLogic;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class AdminController : BaseController
    {
          private readonly IUserMonitoring _monitoring;
          // GET: Admin
          public AdminController()
          {
               var bl = new BussinessLogic();
               _monitoring = bl.GetMonitoringBL();
          }

          [AdminMod]
          public ActionResult Users()
          {
               SessionStatus();
               var allUsers = _monitoring.GetAllUsers();
               return View(allUsers);
          }
        [AdminMod]
        public ActionResult NewUser()
        {
            SessionStatus();
            
            return View();
        }
        [AdminMod]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewUser(ModelNewUser data)
        {
            if (ModelState.IsValid)
            {
                ANewUser uData = new ANewUser
                {
                    Credential = data.Credential,
                    Email = data.Email,
                    Password = data.Password,
                    ConfirmPassword = data.ConfirmPassword,
                    Level = data.Level
                };
                ULoginResp resp = _monitoring.AddNewUser(uData);
                if (resp.Status)
                {
                    return RedirectToAction("Users", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", resp.Message);  // Ensure this property name is correct
                    return RedirectToAction("UserPage", "LoginUser");
                }
            }
            return View(data);
        }

    }
}