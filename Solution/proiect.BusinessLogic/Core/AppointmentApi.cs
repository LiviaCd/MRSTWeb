using proiect.BusinessLogic.DBModel.Seed;
using proiect.Domain.Entities.Appointment;
using proiect.Domain.Entities.News;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace proiect.BusinessLogic.Core
{
     public class AppointmentApi
     {
          public StatusAppointment NewUserAppointment(UAppointment data)
          {
               var userEmail = HttpContext.Current.Session["Username"].ToString();
               int id = 0;
               using (var db = new UserContext())
               {
                    var user = db.Users.FirstOrDefault(u => u.Email == userEmail);
                    if (user == null) return null;
                    id = user.Id;
               }
               using (var db = new AppointmentContext())
               {
                    var newAppointment = new AppointmentDBTable
                    {
                         FirstName = data.FirstName,
                         LastName = data.LastName,
                         Email = data.Email,
                         Address = data.Address,
                         Phone = data.Phone,
                         BloodType = data.BloodType,
                         Date = data.Date,
                         Time = data.Time,
                         IdUser = id,

                    };
                    db.Appointments.Add(newAppointment);
                    db.SaveChanges();
                    return new StatusAppointment { Status = true };
               }
               return new StatusAppointment { Status = false };
          }
          public List<UAppointment> RShowAppointment()
          {
               var userEmail = HttpContext.Current.Session["Username"].ToString();
               int id = 0;
               using (var db = new UserContext())
               {
                    var user = db.Users.FirstOrDefault(u => u.Email == userEmail);
                    if (user == null) return null;
                    id = user.Id;
               }
               using (var dbContext = new AppointmentContext())
               {
                    
                    var appointments = dbContext.Appointments
                        .Where(u => u.IdUser == id)
                        .Select(u => new UAppointment
                        {
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             Email = u.Email,
                             Id = u.AppointmentId,
                             Address = u.Address,
                             Phone = u.Phone,
                             Time = u.Time,
                             Date = u.Date,
                             BloodType = u.BloodType,
                             IdUser = u.IdUser,
                             AppointmentId = u.AppointmentId
                        })
                        .ToList();

                    return appointments;
               }
          }
          public UAppointment RGetAppointmentById(int id)
          {
               UAppointment appToReturn;
               using (var dbContext = new AppointmentContext())
               {
                    var app = dbContext.Appointments.FirstOrDefault(us => us.AppointmentId == id);
                    if (app == null) return null;
                    appToReturn = new UAppointment
                    {
                         Email = app.Email,
                         FirstName = app.FirstName,
                         LastName = app.LastName,
                         Address = app.Address,
                         Phone = app.Phone,
                         Time = app.Time,
                         Date = app.Date,
                         BloodType = app.BloodType,
                         IdUser = app.IdUser,
                         AppointmentId = app.AppointmentId,
                    };
                    return appToReturn;
               }
          }

          public void REditAppointment(int id, UAppointment app)
          {
               using (var dbContext = new AppointmentContext())
               {
                    var appoint = dbContext.Appointments.FirstOrDefault(us => us.AppointmentId == id);
                    if (appoint == null) return;
                    appoint.FirstName = app.FirstName;
                    appoint.LastName = app.LastName;
                    appoint.Email = app.Email;
                    appoint.Address = app.Address;
                    appoint.Phone = app.Phone;
                    appoint.Date = app.Date;
                    appoint.BloodType = app.BloodType;
                    appoint.Time = app.Time;
               
                    dbContext.SaveChanges();
               }
          }

          public void RDeleteAppointment(int id)
          {
               using (var dbContext = new AppointmentContext())
               {
                    var appoint = dbContext.Appointments.FirstOrDefault(us => us.AppointmentId == id);
                    if (appoint == null) return;

                    dbContext.Appointments.Remove(appoint);
                    dbContext.SaveChanges();
               }
          }


     }
}
