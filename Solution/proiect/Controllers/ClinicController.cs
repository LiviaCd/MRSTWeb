using AutoMapper;
using proiect.Attributes;
using proiect.BusinessLogic.DBModel.Seed;
using proiect.Domain.Entities.Ancheta;
using proiect.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class ClinicController : BaseController
    {
          // GET: Clinic
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
     }
}