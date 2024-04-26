using proiect.BusinessLogic.DBModel.Seed;
using proiect.Domain.Entities;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Domain.Enums;
using proiect.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

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
        public ULoginResp AddNewUserAction(ANewUser data)
        {
            using (var db = new UserContext())
            {
                // Check if passwords match first
                if (data.Password != data.ConfirmPassword)
                    return new ULoginResp { Status = false, Message = "Passwords do not match." };

                // Check for existing user
                bool existingUser = db.Users.Any(u => u.UserName == data.Credential);
                if (existingUser)
                {
                    return new ULoginResp { Status = false, Message = "A user already exists with this username." };
                }

                // Create new user
                var newUser = new UDBTable
                {
                    UserName = data.Credential,
                    Email = data.Email,
                    Password = LoginHelper.HashGen(data.Password),
                    LasIp = "",
                    LastLogin = DateTime.Now,
                    Level = URole.User
                };

                // Add new user to database and save changes
                db.Users.Add(newUser);
                try
                {
                    db.SaveChanges();
                    return new ULoginResp { Status = true, Message = "User registered successfully" };
                }
                catch (Exception ex)
                {
                    // Log the exception
                    // Log.Error("Failed to save new user", ex);
                    return new ULoginResp { Status = false, Message = "Error registering user." };
                }
            }
        }

    }
}

