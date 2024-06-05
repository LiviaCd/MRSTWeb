using proiect.Domain.Entities.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect.Models.ChatMessage
{
     public class MChatMessage
     {
          public List<ChatDBTable> ChatsUser { get; set; }
          public string FromUserEmail { get; set; }
          public string ToUserEmail { get; set;}
          public string TextMessage { get; set;}
          public DateTime TimeToSend { get; set;}

     }
}