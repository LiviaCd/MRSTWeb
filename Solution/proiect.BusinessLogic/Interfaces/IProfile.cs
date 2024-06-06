using proiect.Domain.Entities.Profile;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.Interfaces
{
     public interface IProfile
     {
          UserMinimal GetUserByEmail(string email);
          void EditProfile(UserMinimal userMinimal);
          void AddPhotoUser(UserProfileDBTable user);
     }
}
