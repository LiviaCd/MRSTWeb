using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Appointment;
using proiect.Domain.Entities.Profile;
using proiect.Domain.Entities.User;
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
          public List<UAppointment> ShowAppointment(UAppointment data) 
          {
               return RShowAppointment(data);
          }
          public UserMinimal GetUserByEmail(string email)
          {
               return RGetUserByEmail(email);
          }
          public void EditProfile(UserMinimal user) 
          {
               REditProfile(user);
          }

          public void AddPhotoUser(UserProfileDBTable user)
          {
               RAddPhotoUser(user);
          }
     }
}



