using proiect.Domain.Entities.ChatMessage;
using proiect.Domain.Entities.User;
using proiect.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect.Models.ChatMessage
{
     public class ChatPageViewModel
     {
          public List<ChatDBTable> ChatMessages { get; set; }
          public MChatMessage NewMessage { get; set; }
          public List<FindUsers> FindUsers { get; set; }
     }
}