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

namespace proiect.Controllers
{
    public class LoginUserController : BaseController
    {
          // GET: LoginUser
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
               return View();
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

     }
}