using proiect.BusinessLogic.DBModel.Seed;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.Core
{
     public class AdminApi
     {
          public List<UserMinimal> RGetAllUsers()
          {
               using (var dbContext = new UserContext())
               {
                    var users = dbContext.Users.Select(u => new UserMinimal
                    {
                         UserName = u.UserName,
                         Email = u.Email,
                         Id = u.Id,
                         LasIp = u.LasIp,
                         LastLogin = u.LastLogin,
                         Level = u.Level,
                    }).ToList();

                    return users;
               }
          }

          public UserMinimal RGetUserById(int Id)
          {
               using (var dbContext = new UserContext())
               {
                    var user = dbContext.Users.FirstOrDefault(us => us.Id == Id);
                    if (user == null) return null;
                    return new UserMinimal
                    {
                         UserName = user.UserName,
                         Email = user.Email,
                         Level = user.Level,
                         Id = user.Id,
                    };
               }
          }

          public void REditUser(int Id, UserMinimal userModel)
          {
               using (var dbContext = new UserContext())
               {
                    var user = dbContext.Users.FirstOrDefault(us => us.Id == Id);
                    if (user == null) return;

                    user.UserName = userModel.UserName;
                    user.Email = userModel.Email;
                    user.Level = userModel.Level;

                    dbContext.SaveChanges();
               }
          }
     }
}

