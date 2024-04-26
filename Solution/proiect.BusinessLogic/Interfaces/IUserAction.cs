using proiect.Domain.Entities.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.Interfaces
{
    public interface IUserAction
    {
        StatusAppointment UserAppointment(UAppointment data);
    }
}
