﻿using Microsoft.ApplicationInsights.Extensibility.Implementation;
using proiect.Attributes;
using proiect.BusinessLogic;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Domain.Enums;
using proiect.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Email = data.Email,
                    Address = data.Address,
                    Phone = data.Phone,
                    Password = data.Password,
                    ConfirmPassword = data.ConfirmPassword,
                    Level = data.Level
                };
                ULoginResp resp = _monitoring.AddNewUser(uData);
                if (resp.Status)
                {
                         ViewData["ConfirmationMessage"] = "Utilizatorul a fost creat cu succes";
                         return RedirectToAction("TestUsers", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", resp.Message);
                         ViewData["ConfirmationMessage"] = "Utilizatorul nu a fost creat";
                        // return RedirectToAction("NewUser", "Admin");
                }
            }
               return View(data);
        }
          [AdminMod]
          public ActionResult TestUsers()
          {
               SessionStatus();
               var allUsers = _monitoring.GetAllUsers();
               return View(allUsers);
          }

          [AdminMod]
          [HttpGet]
          [Route("Admin/EditUserInfo/{id}")]
          public ActionResult EditUserInfo(int id)
          {
               SessionStatus();
               var userFromDB = _monitoring.RGetUserById(id);
               if (userFromDB == null)
               {
                    return View();
               }
               else
               {
                    return View("EditUserInfo", userFromDB);
               }
          }

          [AdminMod]
          [HttpPost]
          [Route("Admin/EditUserInfo/{id}")]
          [ValidateAntiForgeryToken]
          public ActionResult EditUserInfo(int id, UserMinimal userModel)
          {
               SessionStatus();
               if (ModelState.IsValid)
               {
                    _monitoring.EditUser(id, userModel);
                    return RedirectToAction("TestUsers");
               }
               return View("EditUserInfo", userModel);
          }

          [AdminMod]
          [HttpGet]
          [Route("Admin/BlockUser/{id}")]
          public ActionResult BlockUser(int id)
          {
               SessionStatus();
               var userFromDB = _monitoring.RGetUserById(id);
               if (userFromDB == null)
               {
                    return View();
               }
               else
               {
                    return View("BlockUser", userFromDB);
               }
          }
          [AdminMod]
          [HttpPost]
          [Route("Admin/BlockUser/{id}")]
          [ValidateAntiForgeryToken]
          public ActionResult BlockUser(int id, UserMinimal userModel)
          {
               SessionStatus();
               if (ModelState.IsValid)
               {
               switch(userModel.Option)
                    {
                         case "Blocare 24 ore":
                              {
                                   _monitoring.BlockUser1day(id, userModel);
                                   return RedirectToAction("TestUsers");
                              }
                              break;
                         case "Blocare 72 ore":
                              {
                                   _monitoring.BlockUser3day(id, userModel);
                                   return RedirectToAction("TestUsers");
                              }
                              break;
                         case "Blocare 30 zile":
                              {
                                   _monitoring.BlockUser30day(id, userModel);
                                   return RedirectToAction("TestUsers");
                              }
                              break;
                         case "Blocare permanenta":
                              {
                                   _monitoring.BlockUserPermanent(id, userModel);
                                   return RedirectToAction("TestUsers");
                              }
                              break;
                    }
                   
               }
               return View("BlockUser", userModel);
          }
     }
}