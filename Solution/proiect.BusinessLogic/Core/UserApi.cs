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
                              return new ULoginResp { Status = true };
                    }
               }
               return new ULoginResp { Status = false };
          }
          public ULoginResp RRegisterNewUserAction(URegisterData data)
          {
               var newUser = new UDBTable
               {
                    UserName = data.UserName,
                    Password = data.Password,
                    LasIp = "",
                    LastLogin = DateTime.Now,
                    Level = URole.User
               };

               using (var db = new UserContext())
               {
                    var existingUser = db.Users.FirstOrDefault(u => u.UserName == newUser.UserName);
                    if (existingUser == null)
                    {
                         db.Users.Add(newUser);
                        // db.SaveChanges();
                         //return new ULoginResp { Status = true, Message = "User registered successfully" };
                    }
                   
                   
                    
                    try
                    { 
                         db.SaveChanges();
                         return new ULoginResp { Status = true, Message = "User registered successfully" };
                    }
                    catch (DbEntityValidationException e)
                    {
                         foreach (var eve in e.EntityValidationErrors)
                         {
                              Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                  eve.Entry.Entity.GetType().Name, eve.Entry.State);
                              foreach (var ve in eve.ValidationErrors)
                              {
                                   Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                       ve.PropertyName, ve.ErrorMessage);
                              }
                         }
                    }
                    
                    return new ULoginResp { Status = false, Message = "Error" };
               }
          }

         
          public BloodTypeDetail GetBloodTypeUser (int id)
          {
               return new BloodTypeDetail();
          }

     }
}
