using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Domain.Entities.ChatMessage
{
     public class UserViewChat
     {
          public List<ChatDBTable> ChatMessages { get; set; }
          public DChatMessage NewMessage { get; set; }
          public string FromUserEmail { get; set; }
          public string ToUserEmail { get; set; }
          public string TextMessage { get; set; }
          public DateTime TimeToSend { get; set; }
          public string PhotoPath { get; set; }
          public UserMinimal UserToSend { get; set; }
     }
}
