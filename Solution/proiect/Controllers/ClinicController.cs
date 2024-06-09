using AutoMapper;
using proiect.Attributes;
using proiect.BusinessLogic;
using proiect.BusinessLogic.AppBL;
using proiect.BusinessLogic.DBModel.Seed;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Ancheta;
using proiect.Domain.Entities.BloodR;
using proiect.Domain.Entities.News;
using proiect.Domain.Entities.User;
using proiect.Models.Profile;
using proiect.Models.Request;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace proiect.Controllers
{
    public class ClinicController : BaseController
    {
          private readonly IClinic _clinic;
          public ClinicController()
          {
               var bl = new BussinessLogic();
               _clinic = bl.GetClinicBL();
          }
          [DoctorMod]
          public ActionResult SearchDonator()
          {
               SessionStatus();
               return View();
          }
          public ActionResult DonatorFind()
          {
               var data = TempData["foundData"] as List<Ancheta>;
               return View(data);
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
                    return RedirectToAction("DonatorFind", "Clinic");

               }
               return View(foundData);
          }

          public ActionResult BloodResource()
          {
               SessionStatus();
               var db = new BloodContext();
               string email = Session["Username"].ToString();
               var clinic = db.Resources.FirstOrDefault(u => u.EmailClinic == email);

               if (clinic == null)
               {
                    var clinicToReturn = new BloodResource
                    {
                         EmailClinic = email,
                         OnePozitive = 0,
                         TwoPozitive = 0,
                         ThirdPozitive = 0,
                         FourPozitive = 0,
                         OneNegative = 0,
                         TwoNegative = 0,
                         ThirdNegative = 0,
                         FourNegative = 0,
                         Reservat = false,
                         WhoReservate = null
                    };
                    db.Resources.Add(clinicToReturn);
                    db.SaveChanges();
                    return View(clinicToReturn);
               }
               else
               {
                    return View(clinic);
               }
          }
          [HttpGet]
          public ActionResult EditBloodResource()
          {
               SessionStatus();

               if (Session["Username"] != null)
               {
                    string email = Session["Username"].ToString();
                    var clinic = _clinic.GetClinicByEmail(email);

                    if (clinic != null)
                    {
                         return View(clinic);
                    }
               }
               return View();
          }

          [HttpPost]
          public ActionResult EditBloodResource(BloodResource userMinimal)
          {
               SessionStatus();

               if (userMinimal == null)
               {
                    ModelState.AddModelError(string.Empty, "Invalid user data.");
                    return View(userMinimal);
               }

               var db = new BloodContext();
               var existingClinic = db.Resources.FirstOrDefault(u => u.EmailClinic == userMinimal.EmailClinic);

               if (existingClinic == null)
               {
                    TempData["ErrorMessage"] = "Clinic not found.";
                    return RedirectToAction("BloodResource");
               }

               existingClinic.OnePozitive = userMinimal.OnePozitive;
               existingClinic.TwoPozitive = userMinimal.TwoPozitive;
               existingClinic.ThirdPozitive = userMinimal.ThirdPozitive;
               existingClinic.FourPozitive = userMinimal.FourPozitive;
               existingClinic.OneNegative = userMinimal.OneNegative;
               existingClinic.TwoNegative = userMinimal.TwoNegative;
               existingClinic.ThirdNegative = userMinimal.ThirdNegative;
               existingClinic.FourNegative = userMinimal.FourNegative;

           
               db.SaveChanges();

               TempData["SuccessMessage"] = "Profile updated successfully.";
               return RedirectToAction("BloodResource");
          }

          [DoctorMod]
          
          public ActionResult AllResources()
          {
               SessionStatus(); 
               using (var db = new BloodContext())
               {

                    var resources = db.Resources.ToList();

         
                    return View(resources);
               }
          }

          [DoctorMod]
          [HttpGet]
          [Route("Clinic/ReservBlood/{id}")]
          public ActionResult ReservBlood(int id)
          {
               SessionStatus();
               string email = Session["Username"].ToString();
               using (var dbContext = new BloodContext())
               {
                    var user = dbContext.Resources.FirstOrDefault(us => us.Id == id);
                    if (user == null)
                    {
                         return View();
                    }

                    user.Reservat = true; // Update the property in the retrieved object
                    user.WhoReservate = email;

                    dbContext.SaveChanges();
               }

               return RedirectToAction("AllResources");
          }

          [DoctorMod]
          
          [Route("Clinic/ReservBlood/{id}")]
          [ValidateAntiForgeryToken]
          public ActionResult ReservBlood(int id, BloodResource newsModel)
          {
               SessionStatus();
               if (ModelState.IsValid)
               {
                    _clinic.ReservBlood(id, newsModel);
                    return RedirectToAction("AllResources");
               }
               return View("AllResources", newsModel);
          }

     }
}