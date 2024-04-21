using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.AppBL
{
     public class MonitoringBL : AdminApi, IUserMonitoring 
     {
          public List<UserMinimal> GetAllUsers()
          {
               return RGetAllUsers();
          }

          public UserMinimal GetUserById(int id)
          {
               return RGetUserById(id);
          }

          public void EditUser(int id, UserMinimal user)
          {
               REditUser(id, user);
          }
     }
}
