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
using System.ComponentModel.DataAnnotations;
using System.Web.Management;
using System.Data.Entity;
using proiect.Helpers;
using proiect.Domain.Entities.Session;
using System.Data;
using AutoMapper;
using System.Web;


namespace proiect.BusinessLogic.Core
{
     public class UserApi
     {
          public ULoginResp RLoginUpService(ULoginData data)
          {
               UDBTable user;
              // var validate = new EmailAddressAttribute();
               //if (validate.IsValid(data.Credential))
               //{
                    
                    using (var db = new UserContext())
                    {
                         user = db.Users.FirstOrDefault(us => us.UserName == data.Credential);
                    }
                    if (user == null)
                         return new ULoginResp { Status = false, Message = "The Username or Password is Incorect" };

               var pass = LoginHelper.HashGen(data.Password);
               if (user != null && user.Password == pass)
               {
                    using (var todo = new UserContext())
                    {
                        
                         user.LasIp = data.LoginIp;
                         user.LastLogin = data.LoginDateTime;
                         todo.Entry(user).State = EntityState.Modified;
                         todo.SaveChanges();
                    }
                    if (user.Level == URole.Admin)
                         return new ULoginResp { Status = true, Message = "Admin" };
                    else
                         if (user.Level == URole.Doctor)
                         return new ULoginResp { Status = true, Message = "Doctor" };
                    else
                         return new ULoginResp { Status = true, Message = "User" };
               }
               

              // }
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
                              Password = LoginHelper.HashGen(data.Password),
                              LasIp = "0.0.0.0",
                              LastLogin = DateTime.Now,
                              Level = URole.User
                         };
                    if (data.Password != data.ConfirmPassword)
                         return new ULoginResp { Status = false, Message = "Wrong password" };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return new ULoginResp { Status = true, Message = "User registered successfully" };
               }
          }
     
          public BloodTypeDetail GetBloodTypeUser (int id)
          {
               return new BloodTypeDetail();
          }

          public System.Web.HttpCookie Cookie(string credential)
          {
               var apiCookie = new System.Web.HttpCookie("X-KEY")
               {
                    Value = CookieGenerator.Create(credential)
               };
               using (var db = new SessionContext())
               {
                    Session curent;
                    var validate = new EmailAddressAttribute();
                    if (validate.IsValid(credential))
                    {
                         curent = (from e in db.Sessions where e.UserName == credential select e).FirstOrDefault();
                    }
                    else
                    {
                         curent = (from e in db.Sessions where e.UserName == credential select e).FirstOrDefault();
                    }

                    if (curent != null) 
                    {
                         curent.CookieString = apiCookie.Value;
                         curent.ExpireTime = DateTime.Now.AddMinutes(60);
                         using (var todo = new SessionContext())
                         {
                              todo.Entry(curent).State = EntityState.Modified;
                              todo.SaveChanges();
                         }
                    }
                    else
                    {
                         db.Sessions.Add(new Session { UserName = credential,
                         CookieString = apiCookie.Value,
                         ExpireTime = DateTime.Now.AddMinutes(60) });
                         db.SaveChanges();
                    }
               }
               return apiCookie;
          }
          public UserMinimal UserCookie(string cookie)
          {
               Session session;
               UDBTable curentUser;

               using (var db = new SessionContext())
               {
                    session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
               }

               if (session == null) return null;
               using (var db = new UserContext())
               {
                    var validate = new EmailAddressAttribute();
                    if (validate.IsValid(session.UserName))
                    {
                         curentUser = db.Users.FirstOrDefault(u => u.Email == session.UserName);
                    }
                    else
                    {
                         curentUser = db.Users.FirstOrDefault(u => u.UserName == session.UserName);
                    }
               }
               
               if (curentUser == null) return null;
               var userMinimal = Mapper.Map<UserMinimal>(curentUser);

               return userMinimal;
          }
     }
}
