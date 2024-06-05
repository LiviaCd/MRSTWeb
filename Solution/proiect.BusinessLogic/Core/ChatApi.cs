using proiect.BusinessLogic.DBModel.Seed;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Domain.Entities;
using proiect.Domain.Enums;
using proiect.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiect.Domain.Entities.Profile;
using proiect.Domain.Entities.ChatMessage;
using System.Web;
using System.Web.ModelBinding;

namespace proiect.BusinessLogic.Core
{
     public class ChatAPI
     {
          public void RSendMessage(ChatDBTable chat)
          {
               using (var db = new UserContext())
               {
                    bool toUser = db.Users.Any(u => u.Email == chat.ToUserEmail);
                    if (toUser)
                    {
                         var messageDB = new ChatDBTable
                         {
                              FromUserEmail = chat.FromUserEmail,
                              ToUserEmail = chat.ToUserEmail,
                              TextMessage = chat.TextMessage,
                              TimeToSend = chat.TimeToSend
                         };
                         var dbContext = new ChatMessageContext();

                         dbContext.Chats.Add(messageDB);
                         dbContext.SaveChanges();
                    }
               }
          }

          public List<ChatDBTable> RDisplayAllChat()
          {
               string email = HttpContext.Current.Session["Username"].ToString();
               var db = new ChatMessageContext();
               List<ChatDBTable> existentChat = db.Chats.Where(u => u.FromUserEmail == email).ToList();
               return existentChat;
          }
          public List<UserMinimal> RDisplayAllUser()
          {
               List<UserMinimal> users;
               using (var dbContext = new UserContext())
               {
                    users = dbContext.Users.Select(u => new UserMinimal
                    {
                         FirstName = u.FirstName,
                         LastName = u.LastName,
                         Email = u.Email,
                         PhotoPath = null 
                    }).ToList();
               }

               using (var db = new ProfileContext())
               {
                    var photoProfiles = db.PhotoProfile.ToList(); 

                    
                    foreach (var profile in photoProfiles)
                    {
                         var user = users.FirstOrDefault(u => u.Email == profile.Email); 
                         if (user != null)
                         {
                              user.PhotoPath = profile.PhotoPath; 
                         }
                    }
               }
               return users;
          }

          public UserMinimal RGetUserByEmailChat(int id)
          {
               UDBTable user;
               UserProfileDBTable u;
               var userFind = new UserMinimal();
               using (var dbContext = new UserContext())
               {
                    
                    user = dbContext.Users.FirstOrDefault(us => us.Id == id);
                    if (user == null) return null;
                    else
                    {
                         userFind.FirstName = user.FirstName;
                         userFind.LastName = user.LastName;
                         userFind.Email = user.Email;
                         userFind.Address = user.Address;
                         userFind.Phone = user.Phone;
                         userFind.Password = user.Password;
                         userFind.PhotoPath = null;
                         userFind.LastLogin = user.LastLogin;
                         userFind.BlockTime = user.BlockTime;
                         userFind.LasIp = user.LasIp;
                         userFind.Id = id;
                    }
               }
               using (var db = new ProfileContext())
               {
                    u = db.PhotoProfile.FirstOrDefault(h => h.Email == user.Email);
                    if (u == null) return userFind;
                    else
                         userFind.PhotoPath = u.PhotoPath;
               }
               return userFind;
          }
          public void RSendMessage(int id, UserViewChat model)
          {
               var user = new UDBTable();
               string email;
               var dbContext = new UserContext();
               {
                    user = dbContext.Users.FirstOrDefault(u => u.Id == id);
                    if (user == null) return;
                    else
                         email = user.Email;
                
            }
                    var chat = new ChatDBTable
                    {
                         FromUserEmail = HttpContext.Current.Session["Username"].ToString(),
                         ToUserEmail = email,
                         TextMessage = model.TextMessage,
                         TimeToSend = DateTime.Now
                    };
               var db = new ChatMessageContext();
               db.Chats.Add(chat);
               db.SaveChanges();
          }



     }
}
