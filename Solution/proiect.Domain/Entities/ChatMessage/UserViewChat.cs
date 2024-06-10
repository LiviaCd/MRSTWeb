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
          public UserMinimal UserToSend { get; set; }
         
     }
}
