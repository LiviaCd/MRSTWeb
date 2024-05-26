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

namespace proiect.Controllers
{
    public class LoginUserController : BaseController
    {
        private readonly IUserAction _userAction;
        
          // GET: LoginUser
          public LoginUserController()
        {
            var bl = new BussinessLogic();
            _userAction = bl.GetUserActionBL();
          
          }
        [LoginUserMod]
        public ActionResult Appointment()
        {
            SessionStatus();
            return View();
        }
          [LoginUserMod]
          public ActionResult MyAppointments()
          {
               SessionStatus();
               return View();
          }

          [LoginUserMod]
          public ActionResult UserPageNow()
          {
               SessionStatus();
               return View();
          }
          [DoctorMod]
          public ActionResult SearchDonator()
          {
               SessionStatus();
               return View();
          }
          [LoginUserMod]
          public ActionResult UserPage()
          {
               SessionStatus();
               /*
               DateTime? blockTime = HttpContext.Session["BlockTime"] as DateTime?;

               if (blockTime < DateTime.Now)
               {
                    return RedirectToAction("UserPageNow", "LoginUser");
               }
               else
               {
                    return RedirectToAction("AccountBlock", "LoginUser");
               }
               */
               return View();
          }


          public ActionResult DonatorFind()
          {
               var data = TempData["foundData"] as List<Ancheta>; 
               return View(data);
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

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult SearchDonator(MRequest request)
          {
               var dataRequest = Mapper.Map<Ancheta>(request);
               List<Ancheta> foundData = null;

               using (var db = new AnchetaContext())
               {
                    foundData = db.Anchete
    .Where(e => e.BloodType == request.BloodType && (request.City == null || e.City == request.City))
    .ToList();

               }

               if (foundData == null || !foundData.Any())
               {
                    TempData["ConfirmationMessage"] = "Nu am fost gasiti donatori";
               }
               else
               {
                    TempData["foundData"] = foundData; 
                    return RedirectToAction("DonatorFind", "LoginUser");

               }
               return View(foundData);
          }


          [LoginUserMod]
          public ActionResult AnchetaPage()
          {
               SessionStatus();
               return View();
          }
          
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult AnchetaPage (MAncheta ancheta)
          {
               var validate = new PhoneAttribute();
                    if (validate.IsValid(ancheta.Phone))
                    {
                         var dataAncheta = Mapper.Map<Ancheta>(ancheta);

                         dataAncheta.FirstName = ancheta.FirstName;
                         dataAncheta.LastName = ancheta.LastName;
                         dataAncheta.Age = ancheta.Age;
                         dataAncheta.Gender = ancheta.Gender;
                         dataAncheta.BloodType = ancheta.BloodType;
                         dataAncheta.Email = ancheta.Email;
                         dataAncheta.City = ancheta.City;
                         dataAncheta.CityRural = ancheta.CityRural;
                         dataAncheta.District = ancheta.District;
                         dataAncheta.Phone = ancheta.Phone;
                    using (var db = new AnchetaContext())
                    {
                         dataAncheta = (from e in db.Anchete where e.Phone == ancheta.Phone select e).FirstOrDefault();
                         if (dataAncheta == null)
                         {
                              db.Anchete.Add(new Ancheta
                              {
                                   FirstName = ancheta.FirstName,
                                   LastName = ancheta.LastName,
                                   Age = ancheta.Age,
                                   Gender = ancheta.Gender,
                                   BloodType = ancheta.BloodType,
                                   Email = ancheta.Email,
                                   City = ancheta.City,
                                   CityRural = ancheta.CityRural,
                                   District = ancheta.District,
                                   Phone = ancheta.Phone
                              });
                              db.SaveChanges();
                              ViewData["ConfirmationMessage"] = "Your submission has been processed successfully";
                         }
                         else
                         {
                              ViewData["ConfirmationMessage"] = "An inquiry with this phone number already exists";
                         }
                    }
               }
               return View(ancheta);
          }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Appointment(Appointment data)
        {
            if (ModelState.IsValid)
            {
                UAppointment uData = new UAppointment
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Email = data.Email,
                    Address = data.Address,
                    Phone = data.Phone,
                    BloodType = data.BloodType,
                    Time = data.Time,
                };
                StatusAppointment resp = _userAction.UserAppointment(uData);
                if (resp.Status)
                {
                         ViewData["ConfirmationMessage"] = "Programarea dumneavoastra a fost salvata cu succes!";
                         return View(data);
                }
                else
                {
                         ViewData["ConfirmationMessage"] = "Din pacate, programarea dvs. nu a fost salvata";
                         return View();
                }
            }
            return View(data);
        }
         public ActionResult MyAppointments (Appointment data)
          {
               if (ModelState.IsValid) 
               {
                    UAppointment uData = new UAppointment
                    {
                         FirstName = data.FirstName,
                         LastName = data.LastName,
                         Email = data.Email,
                         Address = data.Address,
                         Phone = data.Phone,
                         BloodType = data.BloodType,
                         Time = data.Time,
                    };
                   _userAction.ShowAppointment(uData);
               }
               return View(data);
          }

          public ActionResult ProfilePage()
          {
               SessionStatus();
               if (Session["Username"] != null)
               {
                    string email = Session["Username"].ToString();
                    var userFromDB = _userAction.GetUserByEmail(email);
                    if (userFromDB != null)
                    {
                         return View(userFromDB);
                    }
               }
               return RedirectToAction("LogIn", "Login");
          }

          [AdminMod]
          [HttpPost]
          public ActionResult ProfilePage(UserMinimal userMinimal)
          {
               SessionStatus();
               var userFromDB = _userAction.GetUserByEmail(userMinimal.Email);
               if (userFromDB == null)
               {
                    return View();
               }
               else
               {
                    return View(userFromDB);
               }
          }
          [HttpGet]
          public ActionResult EditProfile()
          {
               SessionStatus();

               if (Session["Username"] != null)
               {
                    string email = Session["Username"].ToString();
                    var userFromDB = _userAction.GetUserByEmail(email);

                    if (userFromDB != null)
                    {
                         var userModel = new UserMinimal
                         {
                              Email = userFromDB.Email,
                              FirstName = userFromDB.FirstName,
                              LastName = userFromDB.LastName,
                              Address = userFromDB.Address,
                              Phone = userFromDB.Phone,
                              LastLogin = userFromDB.LastLogin,
                              Level = userFromDB.Level,
                              LasIp = userFromDB.LasIp,
                         };

                         return View(userModel);
                    }
               }

               return RedirectToAction("LogIn", "Login");
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult EditProfile(UserMinimal userMinimal)
          {
               SessionStatus();

               if (userMinimal == null)
               {
                    ModelState.AddModelError(string.Empty, "Invalid user data.");
                    return View(userMinimal);
               }

               _userAction.EditProfile(userMinimal);
               TempData["SuccessMessage"] = "Profile updated successfully.";
               return RedirectToAction("ProfilePage");
          }

     }

}