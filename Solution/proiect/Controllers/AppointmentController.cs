using proiect.Attributes;
using proiect.BusinessLogic;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Appointment;
using proiect.Domain.Entities.News;
using proiect.Domain.Enums;
using proiect.Models.Appointment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class AppointmentController : BaseController
    {
          private readonly IAppointment _appointment;
          public AppointmentController()
          {
               var bl = new BussinessLogic();
               _appointment = bl.GetAppointmentBL();
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
               var app = _appointment.ShowAppointment();
               return View(app);
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
                         Date = data.Date,
                         Time = data.Time,
                         IdUser = data.IdUser,
                    };
                    StatusAppointment resp = _appointment.UserAppointment(uData);
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

          [LoginUserMod]
          [HttpGet]
          [Route("Appointment/EditAppointment/{id}")]
          public ActionResult EditAppointment(int id)
          {
               SessionStatus();
               var userFromDB = _appointment.GetAppointmentById(id);
               if (userFromDB == null)
               {
                    return View();
               }
               else
               {
                    return View("EditAppointment", userFromDB);
               }
          }

          [AdminMod]
          [HttpPost]
          [Route("Appointment/EditAppointment/{id}")]
          [ValidateAntiForgeryToken]
          public ActionResult EditAppointment(int id, Appointment appModel)
          {
               SessionStatus();
               UAppointment uData = new UAppointment();
               if (ModelState.IsValid)
               {
                    uData = new UAppointment
                    {
                         FirstName = appModel.FirstName,
                         LastName = appModel.LastName,
                         Email = appModel.Email,
                         Address = appModel.Address,
                         Phone = appModel.Phone,
                         BloodType = appModel.BloodType,
                         Date = appModel.Date,
                         Time = appModel.Time,
                         IdUser = appModel.IdUser,
                         AppointmentId = appModel.AppointmentId,
                    };
                    _appointment.EditAppointment(id, uData);
                    return RedirectToAction("Appointment");
               }
               return View("EditAppointment", uData);
          }

          [LoginUserMod]
          [HttpGet]
          [Route("Appointment/DeleteAppointment/{id}")]
          public ActionResult DeleteAppointment(int id)
          {
               SessionStatus();
               var userFromDB = _appointment.GetAppointmentById(id);
               if (userFromDB == null)
               {
                    return View();
               }
               else
               {
                    return View("DeleteAppointment", userFromDB);
               }
          }

          [AdminMod]
          [HttpPost]
          [Route("Appointment/DeleteAppointment/{id}")]
          [ValidateAntiForgeryToken]
          public ActionResult DeleteAppointment(int id, Appointment appModel)
          {
               SessionStatus();
               UAppointment uData = new UAppointment();
               if (ModelState.IsValid)
               {
                    _appointment.DeleteAppointment(id);
                    return RedirectToAction("MyAppointments");
               }
               return View("DeleteAppointment", uData);
          }

     }
}