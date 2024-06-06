using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.Interfaces;
using proiect.BusinessLogic;
using proiect.BusinessLogic.DBModel.Seed;
using proiect.BusinessLogic.AppBL;

namespace proiect.BusinessLogic
{
     public class BussinessLogic
     {
          public ISession GetSessionBL()
          {
               return new SessionBL();
          }
          public IUserMonitoring GetMonitoringBL()
          {
               return new MonitoringBL();
          }
        public IProfile GetProfileBL()
        {
            return new ProfileBL();
        }
          public IAncheta GetAnchetaBL()
          {
               return new AnchetaBL();
          }
          public IAppointment GetAppointmentBL()
          {
               return new AppointmentBL();
          }
          public IClinic GetClinicBL()
          {
               return new ClinicBL();
          }
          public IChatMessage GetChatBL() 
          {
               return new ChatBL();
          }

    }
}
