using proiect.Attributes;
using proiect.BusinessLogic;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Appointment;
using proiect.Models.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
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
               return View();
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
          public ActionResult MyAppointments(Appointment data)
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
                    _appointment.ShowAppointment(uData);
               }
               return View(data);
          }
     }
}