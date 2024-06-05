using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Domain.Entities.ChatMessage
{
     public class DChatMessage
     {
          public string FromUserEmail { get; set; }
          public string ToUserEmail { get; set; }
          public string TextMessage { get; set; }
          public DateTime TimeToSend { get; set; }
     }
}
