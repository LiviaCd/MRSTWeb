using proiect.Domain.Entities.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.Interfaces
{
     public interface IAppointment
     {
          StatusAppointment UserAppointment(UAppointment data);
          List<UAppointment> ShowAppointment();
          UAppointment GetAppointmentById (int id);
          void EditAppointment(int id, UAppointment action);
          void DeleteAppointment(int id);
     }
}
