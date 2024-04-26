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
    public class UserActionBL : UserApi, IUserAction
    {
        public StatusAppointment UserAppointment(UAppointment data)
        {
            return NewUserAppointment(data);
        }
    }
}
