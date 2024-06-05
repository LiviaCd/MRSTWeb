using proiect.BusinessLogic;
using proiect.BusinessLogic.DBModel.Seed;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.ChatMessage;
using proiect.Domain.Entities.Profile;
using proiect.Domain.Entities.User;
using proiect.Models.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace proiect.Controllers
{
    public class ChatMessageController : BaseController
    {
          // GET: ChatMessage
          private readonly IChatMessage _conversation;

          public ChatMessageController()
          {
               var bl = new BussinessLogic();
               _conversation = bl.GetChatBL();
          }
          public ActionResult ChatPage()
          {
               string email = Session["Username"].ToString();
               var model = new ChatPageViewModel
               {
                    ChatMessages = GetMessagesByEmail(),
                    NewMessage = new MChatMessage() // Inițializați noul mesaj aici dacă este nevoie
               };
               return View(model);
          }
          public ActionResult SearchUser()
          {
               SessionStatus();
               var allUsers = _conversation.DisplayAllUser();
               return View(allUsers);  
          }
          /*
          [HttpGet]
          public ActionResult SearchUser(string searchTerm)
          {
               var viewModel = new ChatPageViewModel
               {
                    ChatMessages = new List<ChatDBTable>(), // Assuming you initialize it empty or with some data
                    NewMessage = new MChatMessage(), // Assuming you initialize it empty or with some data
                    FindUsers = new List<FindUsers>()
               };

               using (var userDb = new UserContext())
               {
                    var users = userDb.Users
                                      .Where(user => user.FirstName.Contains(searchTerm) ||
                                                     user.LastName.Contains(searchTerm) ||
                                                     user.Email.Contains(searchTerm))
                                      .ToList();

                    // Extract emails from the users list
                    var userEmails = users.Select(user => user.Email).ToList();

                    using (var profileDb = new ProfileContext())
                    {
                         var profiles = profileDb.PhotoProfile
                                                 .Where(profile => userEmails.Contains(profile.Email))
                                                 .ToList();

                         viewModel.FindUsers = users.Select(user => new FindUsers
                         {
                              Email = user.Email,
                              FirstName = user.FirstName,
                              LastName = user.LastName,
                              PhotoPath = profiles.FirstOrDefault(profile => profile.Email == user.Email)?.PhotoPath ?? "~/Content/PhotosUsers/user.png"
                         }).ToList();
                    }
               }

               return RedirectToAction("SearchPage", "ChatMessage");
          }
          */
          [HttpGet]
          [Route("ChatMessage/SendMessageUser/{id}")]
          public ActionResult SendMessageUser(int id)
          {
               SessionStatus();
               UserMinimal userFromDB = _conversation.GetUserByEmailChat(id);
               if (userFromDB == null)
               {
                    return View();
               }
               else
               {
                    return View("SendMessageUser", userFromDB);
               }
          }
          [HttpPost]
          [Route("ChatMessage/SendMessageUser/{id}")]
          [ValidateAntiForgeryToken]
          public ActionResult SendMessageUser(int id, UserViewChat userModel)
          {
               SessionStatus();
               if (ModelState.IsValid)
               {
                    _conversation.SendMessage(id, userModel);
                    return RedirectToAction("ChatPage");
               }
               return View("SendMessageUser", userModel);
          }
          
          private List<ChatDBTable> RDisplayAllChat()
          {
               string email = Session["Username"].ToString();
               var db = new ChatMessageContext();
               var existentChat = db.Chats
                   .Where(u => u.FromUserEmail == email || u.ToUserEmail == email)
                   .GroupBy(u => u.FromUserEmail == email ? u.ToUserEmail : u.FromUserEmail)
                   .Select(g => g.OrderByDescending(m => m.TimeToSend).FirstOrDefault())
                   .ToList();
               return existentChat;
          }
          public List<ChatDBTable> GetMessagesByEmail()
          {
               string email = Session["Username"].ToString();
               using (var db = new ChatMessageContext())
               {
                    // Obținem toate mesajele care implică email-ul dat
                    List<ChatDBTable> messages = db.Chats
                        .Where(u => u.FromUserEmail == email || u.ToUserEmail == email)
                        .ToList();

                    // Grupăm toate mesajele implicate, indiferent cine le-a trimis
                    var allMessages = messages
                        .GroupBy(u => u.FromUserEmail == email ? u.ToUserEmail : u.FromUserEmail)
                        .Select(g => g.OrderByDescending(m => m.TimeToSend).FirstOrDefault())
                        .ToList();

                    return allMessages;
               }
          }




          /*
          private List<ChatDBTable> DisplayAllUserChat()
          {
               string email = Session["Username"].ToString();
               var db = new ChatMessageContext();
               var existentChat = db.Chats
                                    .Where(u => u.FromUserEmail == email || u.ToUserEmail == email)
                                    .GroupBy(u => u.ToUserEmail)
                                    .Select(g => g.OrderByDescending(u => u.TimeToSend).FirstOrDefault())
                                    .ToList();
               return existentChat;
          }
          */
     }
}