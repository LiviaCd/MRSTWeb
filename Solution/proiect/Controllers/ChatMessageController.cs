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
using System.Web.Services.Description;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace proiect.Controllers
{
     public class ChatMessageController : BaseController
     {
          private readonly IChatMessage _conversation;

          public ChatMessageController()
          {
               var bl = new BussinessLogic();
               _conversation = bl.GetChatBL();
          }
          public List<string> GetRecentUsers(string currentUserEmail)
          {
               var db = new ChatMessageContext();
               var recentUsers = db.Chats
                   .Where(m => m.FromUserEmail == currentUserEmail || m.ToUserEmail == currentUserEmail)
                   .OrderByDescending(m => m.TimeToSend)
                   .Select(m => m.FromUserEmail == currentUserEmail ? m.ToUserEmail : m.FromUserEmail)
                   .Distinct()
                   .Take(10) 
                   .ToList();

               return recentUsers;
          }

          public ActionResult ChatPage()
          {
               string email = Session["Username"].ToString();

               using (var db = new ChatMessageContext())
               {
                    var chatsPrimite = db.Chats
                        .Where(msg =>
                            (msg.FromUserEmail != email && msg.ToUserEmail == email))
                        .OrderByDescending(msg => msg.TimeToSend) // Sortează mesajele descrescător după timp
                        .ToList();

                    return View(chatsPrimite);
               }
          }

          public ActionResult SendMessage()
          {
               string email = Session["Username"].ToString();

               using (var db = new ChatMessageContext())
               {
                    var chatsPrimite = db.Chats
                        .Where(msg =>
                            (msg.FromUserEmail == email && msg.ToUserEmail != email))
                        .OrderByDescending(msg => msg.TimeToSend) // Sortează mesajele descrescător după timp
                        .ToList();

                    return View(chatsPrimite);
               }
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult NewMessage(ChatDBTable message)
          {
               string email = Session["Username"].ToString();
               var db = new ChatMessageContext();
               if (ModelState.IsValid)
               {
                    var messageDB = new ChatDBTable
                    {
                         FromUserEmail = email,
                         ToUserEmail = message.ToUserEmail,
                         TextMessage = message.TextMessage,
                         TimeToSend = DateTime.Now
                    };
                    db.Chats.Add(messageDB);
                    db.SaveChanges();

                    return RedirectToAction("SendMessage", "ChatMessage"); 
               }

               return View("NewMessage", message);
          }
          public ActionResult SearchUser()
          {
               SessionStatus();
               var allUsers = _conversation.DisplayAllUser();
               return View(allUsers);
          }
          public ActionResult NewMessage()
          {
               SessionStatus();

               return View();
          }

          [HttpGet]
          [Route("ChatMessage/SendMessageUser/{id}")]
          public ActionResult SendMessageUser(int id)
          {
               SessionStatus();
               UserMinimal userFromDB = _conversation.GetUserByEmailChat(id);
               if (userFromDB == null)
               {
                    return View(new UserMinimal()); // Returnăm o instanță goală dacă utilizatorul nu există
               }
               else
               {
                    var userModel = new UserViewChat
                    {
                         UserToSend = userFromDB,
                         NewMessage = new DChatMessage() // Inițializăm un nou mesaj pentru a evita NullReferenceException
                    };
                    return View("SendMessageUser", userModel);
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
                    // Obțineți toate mesajele între utilizatorul curent și utilizatorul găsit după ID
                    var currentUserEmail = Session["Username"].ToString();
                    var messages = GetMessagesBetweenUsers(currentUserEmail, id);

                    // Adăugați logica pentru trimiterea mesajului, în cazul în care este necesar
                    _conversation.SendMessage(id, userModel);

                    // Afișați mesajele și returnați la pagina de chat
                    userModel.ChatMessages = messages; // Asigurați-vă că modelul are lista completă de mesaje
                    //return RedirectToAction("ChatPage", userModel);
               }
               return View("SendMessageUser", userModel);
          }

          public List<ChatDBTable> GetMessagesBetweenUsers(string currentUserEmail, int otherUserId)
          {
               string email = null;
               var dbContext = new UserContext();
               var user = dbContext.Users.FirstOrDefault(u => u.Id == otherUserId);
               if (user != null)
                    email = user.Email;
               var db = new ChatMessageContext();
               var messages = db.Chats
                   .Where(msg =>
                       (msg.FromUserEmail == currentUserEmail && msg.ToUserEmail == email) ||
                       (msg.FromUserEmail == email && msg.ToUserEmail == currentUserEmail))
                   .ToList();

               return messages;
          }
          // Metoda pentru adăugarea unui utilizator în lista de utilizatori recente
          private void AddUserToRecentUsers(int userId)
          {
               // Implementează logica pentru adăugarea utilizatorului în lista de utilizatori recente,
               // probabil ar trebui să fie stocată într-un serviciu sau în sesiune, depinzând de necesitățile aplicației tale
          }


          private List<ChatDBTable> GetMessagesByEmail()
          {
               string email = Session["Username"].ToString();
               using (var db = new ChatMessageContext())
               {
                    List<ChatDBTable> messages = db.Chats
                        .Where(u => u.FromUserEmail == email || u.ToUserEmail == email)
                        .ToList();

                    // Grupăm mesajele după adresa de email a destinatarului sau expeditorului,
                    // apoi selectăm cel mai recent mesaj din fiecare grupă
                    var allMessages = messages
                        .GroupBy(u => u.FromUserEmail == email ? u.ToUserEmail : u.FromUserEmail)
                        .Select(g => g.OrderByDescending(m => m.TimeToSend).FirstOrDefault())
                        .ToList();

                    return allMessages;
               }
          }



     }

}