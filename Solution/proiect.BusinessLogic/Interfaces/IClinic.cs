using proiect.Domain.Entities.BloodR;
using proiect.Domain.Entities.News;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.Interfaces
{
     public interface IClinic
     {
          BloodResource GetClinicByEmail(string email);
          void EditProfile(BloodResource blood);
          BloodResource GetClinicById(int Id);
          void ReservBlood(int id, BloodResource blood);
     }
}
