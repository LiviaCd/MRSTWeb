using proiect.Attributes;
using proiect.Domain.Entities.Ancheta;
using proiect.Models.Ancheta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.DBModel.Seed;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using proiect.Domain.Entities.User;
using System.Web.Helpers;
using System.Web.UI;
using System.Data.Entity.Validation;
using proiect.Models.Request;
using proiect.BusinessLogic.Interfaces;
using proiect.BusinessLogic;
using proiect.Domain.Entities.Appointment;
using proiect.Models.Appointment;
using static System.Net.WebRequestMethods;
using System.Web.UI.WebControls;
using System.Web.Security;
using proiect.Models.Profile;
using System.IO;
using proiect.Domain.Entities.Profile;
using proiect.BusinessLogic.AppBL;
using proiect.Extensions;

namespace proiect.Controllers
{
     public class LoginUserController : BaseController
     {

          // GET: LoginUser
          public LoginUserController()
          {

          }
          

          [LoginUserMod]
          public ActionResult UserPageNow()
          {
               SessionStatus();
               return View();
          }
          
          [LoginUserMod]
          public ActionResult UserPage()
          {
               SessionStatus();
               DateTime? blockTime = Session["BlockTime"] as DateTime?;
               if (blockTime == new DateTime(2000, 1, 1))
               {
                    return RedirectToAction("ChangePasswordAdmin", "Login");
               }
               return View();
          }


          

          public ActionResult LocationsPage()
          {
               SessionStatus();
               return View();
          }

          public ActionResult NewsUserPage()
          {
               SessionStatus();
               return View();
          }



          public ActionResult AccountBlock()
          {
               SessionStatus();
               return View();
          }
    
     }
}
