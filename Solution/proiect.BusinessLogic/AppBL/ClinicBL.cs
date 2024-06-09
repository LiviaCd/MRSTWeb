using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.BloodR;
using proiect.Domain.Entities.News;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.AppBL
{
     public class ClinicBL : ClinicApi, IClinic
     {
          public BloodResource GetClinicByEmail(string email)
          {
               return RGetClinicByEmail(email);
          }
          public void EditProfile(BloodResource blood)
          {
               REditProfile(blood);
          }
          public BloodResource GetClinicById(int Id)
          {
               return RGetClinicById(Id);
          }
          public void ReservBlood(int id, BloodResource news)
          {
               RReservBlood(id, news);
          }
     }
}
