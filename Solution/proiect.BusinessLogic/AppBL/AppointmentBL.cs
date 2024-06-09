using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.AppBL
{
     public class AppointmentBL : AppointmentApi, IAppointment
     {
          public StatusAppointment UserAppointment(UAppointment data)
          {
               return NewUserAppointment(data);
          }
          public List<UAppointment> ShowAppointment()
          {
               return RShowAppointment();
          }
          public UAppointment GetAppointmentById(int id)
          {
               return RGetAppointmentById(id);
          }
          public void EditAppointment(int id, UAppointment app)
          {
               REditAppointment(id, app);
          }
          public void DeleteAppointment(int id) 
          {
               RDeleteAppointment(id);
          }
     }
}
