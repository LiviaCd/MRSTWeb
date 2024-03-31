using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Domain.Enums;
using proiect.Domain.Entities.BloodType;
using proiect.BusinessLogic.DBModel.Seed;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using proiect.Domain.Entities;
using System.Data.Entity.Validation;
using System.Runtime.Remoting.Contexts;


namespace proiect.BusinessLogic.Core
{
     public class UserApi
     {
          public ULoginResp RLoginUpService(ULoginData data)
          {
               UDBTable user;
               using (var db = new UserContext())
               {
                    user = db.Users.FirstOrDefault(us => us.UserName == data.Credential);
                    if (user == null) return new ULoginResp { Status = false };
                    else
                    {
                         if (user.Password == data.Password)
                         {
                              if (user.Level == URole.Admin)
                                   return new ULoginResp { Status = true, Message = "Admin" };
                              
                              return new ULoginResp { Status = true};
                         }
                              
                    }
               }
               return new ULoginResp { Status = false };
          }
          public ULoginResp RRegisterNewUserAction(URegisterData data)
          {
               using (var db = new UserContext())
               {
                    bool existingUser = db.Users.Any(u => u.UserName == data.Credential);

                    if (existingUser)
                    {
                         return new ULoginResp { Status = false, Message = "A user exist" };
                    }
                         var newUser = new UDBTable
                         {
                              UserName = data.Credential,
                              Email = data.Email,
                              Password = data.Password,
                              LasIp = "",
                              LastLogin = DateTime.Now,
                              Level = URole.User
                         };
                    if (data.Password != data.ConfirmPassword)
                         return new ULoginResp { Status = false, Message = "Wrong password" };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return new ULoginResp { Status = true, Message = "User registered successfully" };
               }
               //return new ULoginResp { Status = false, Message = "Error" };
          }
     
          public BloodTypeDetail GetBloodTypeUser (int id)
          {
               return new BloodTypeDetail();
          }

     }
}
