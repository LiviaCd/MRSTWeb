using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Profile;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.AppBL
{
     public class ProfileBL : ProfileApi, IProfile
     {
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
