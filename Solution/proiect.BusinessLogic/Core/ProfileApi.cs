using proiect.BusinessLogic.DBModel.Seed;
using proiect.Domain.Entities.Profile;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace proiect.BusinessLogic.Core
{
     public class ProfileApi
     {
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
