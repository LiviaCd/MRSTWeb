using proiect.BusinessLogic.DBModel.Seed;
using proiect.Domain.Entities.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.Core
{
     public class AppointmentApi
     {
          public StatusAppointment NewUserAppointment(UAppointment data)
          {
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
                         Time = data.Time,

                    };
                    db.Appointments.Add(newAppointment);
                    db.SaveChanges();
                    return new StatusAppointment { Status = true };
               }
               return new StatusAppointment { Status = false };
          }
          public List<UAppointment> RShowAppointment(UAppointment data)
          {
               using (var dbContext = new AppointmentContext())
               {
                    var appoint = dbContext.Appointments.Select(u => new UAppointment
                    {
                         FirstName = u.FirstName,
                         LastName = u.LastName,
                         Email = u.Email,
                         Id = u.AppointmentId,
                         Address = u.Address,
                         Phone = u.Phone,
                         Time = u.Time,
                         BloodType = u.BloodType,

                    }).ToList();

                    return appoint;
               }
          }
     }
}
