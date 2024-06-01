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
using System.Net;
using proiect.Domain.Entities.Ancheta;
using proiect.Domain.Entities.Appointment;
using proiect.Domain.Entities.Profile;


namespace proiect.BusinessLogic.Core
{
     public class UserApi
     {
          public ULoginResp RLoginUpService(ULoginData data)
          {
               UDBTable user;
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {

                    using (var db = new UserContext())
                    {
                         user = db.Users.FirstOrDefault(us => us.Email == data.Email);
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


               }
               return new ULoginResp { Status = false };
          }



          public ULoginResp RRegisterNewUserAction(URegisterData data)
          {
               using (var db = new UserContext())
               {
                    bool existingUser = db.Users.Any(u => u.Email == data.Email);

                    if (existingUser)
                    {
                         return new ULoginResp { Status = false, Message = "A user exist" };
                    }
                    var newUser = new UDBTable
                    {
                         FirstName = data.FirstName,
                         LastName = data.LastName,
                         Email = data.Email,
                         Address = data.Address,
                         Phone = data.Phone,
                         Password = LoginHelper.HashGen(data.Password),
                         LasIp = "0.0.0.0",
                         LastLogin = DateTime.Now,
                         Level = URole.User,
                         BlockTime = new DateTime(1900, 1, 1)
                    };
                    if (data.Password != data.ConfirmPassword)
                         return new ULoginResp { Status = false, Message = "Wrong password" };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return new ULoginResp { Status = true, Message = "User registered successfully" };
               }
          }

          public BloodTypeDetail GetBloodTypeUser(int id)
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
                         db.Sessions.Add(new Session
                         {
                              UserName = credential,
                              CookieString = apiCookie.Value,
                              ExpireTime = DateTime.Now.AddMinutes(60)
                         });
                         db.SaveChanges();
                    }
               }
               return apiCookie;
          }
          public UserMinimal UserCookie(string cookie)
          {
               Session session;
               UDBTable curentUser;
               UserProfileDBTable userProfile;
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
                         curentUser = db.Users.FirstOrDefault(u => u.Email == session.UserName);
                    }
               }

               if (curentUser == null) return null;

               var userMinimal = Mapper.Map<UserMinimal>(curentUser);
               using (var dbContext = new ProfileContext())
               {
                    userProfile = dbContext.PhotoProfile.FirstOrDefault(p => p.Email == curentUser.Email);
                    if (userProfile == null)
                         return userMinimal;
                    else
                    {
                         userMinimal.PhotoPath = userProfile.PhotoPath;

                    }


               }
               return userMinimal;
          }
          public StatusAppointment NewUserAppointment(UAppointment data)
          {
               using (var db = new AppointmentContext())
               {
                    var newAppointment = new AppointmentDBTable
                    {
                         FirstName = data.FirstName,
                         LastName = data.LastName,
                         Email = data.Email,
                         Address = data.Address,
                         Phone = data.Phone,
                         BloodType = data.BloodType,
                         Time = data.Time,

                    };
                    db.Appointments.Add(newAppointment);
                    db.SaveChanges();
                    return new StatusAppointment { Status = true };
               }
               return new StatusAppointment { Status = false };
          }
          public List<UAppointment> RShowAppointment(UAppointment data)
          {
               using (var dbContext = new AppointmentContext())
               {
                    var appoint = dbContext.Appointments.Select(u => new UAppointment
                    {
                         FirstName = u.FirstName,
                         LastName = u.LastName,
                         Email = u.Email,
                         Id = u.AppointmentId,
                         Address = u.Address,
                         Phone = u.Phone,
                         Time = u.Time,
                         BloodType = u.BloodType,

                    }).ToList();

                    return appoint;
               }
          }
          public UserMinimal RGetUserByEmail(string email)
          {
               var user = new UserMinimal();
               using (var dbContext = new UserContext())
               {
                    var userData = dbContext.Users.FirstOrDefault(u => u.Email == email);
                    if (userData == null)
                         return null;
                    else
                    {
                         user = new UserMinimal
                         {
                              FirstName = userData.FirstName,
                              LastName = userData.LastName,
                              Email = userData.Email,
                              Address = userData.Address,
                              Phone = userData.Phone,
                              LastLogin = userData.LastLogin,
                              LasIp = userData.LasIp,
                         };

                         using (var db = new ProfileContext())
                         {
                              var userPhoto = db.PhotoProfile.FirstOrDefault(u => u.Email == email);
                              if (userPhoto == null) return user;
                              else
                              {
                                   user.PhotoPath = userPhoto.PhotoPath;
                              }

                         }
                         return user;
                    }
               }
          }
          public void REditProfile(UserMinimal userModel)
          {
               using (var dbContext = new UserContext())
               {
                    string email = HttpContext.Current.Session["Username"].ToString();
                    var user = dbContext.Users.FirstOrDefault(us => us.Email == email);
                    if (user == null) return;

                    user.FirstName = userModel.FirstName;
                    user.LastName = userModel.LastName;
                    user.Address = userModel.Address;
                    user.Phone = userModel.Phone;


                    dbContext.SaveChanges();
               }
          }
          public void RAddPhotoUser(UserProfileDBTable profileTable)
          {
               using (var dbContext = new ProfileContext())
               {
                    var user = dbContext.PhotoProfile.SingleOrDefault(us => us.Email == profileTable.Email);
                    if (user == null)
                    {
                         dbContext.PhotoProfile.Add(profileTable);
                    }
                    else
                    {
                         user.PhotoPath = profileTable.PhotoPath; // Update the existing record
                         dbContext.Entry(user).State = EntityState.Modified; // Mark entity as modified
                    }
                    dbContext.SaveChanges();
               }
          }


     }
}

