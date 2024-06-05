using proiect.Domain.Entities.ChatMessage;
using proiect.Domain.Entities.Profile;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.DBModel.Seed
{
     public class ChatMessageContext : DbContext
     {
          public ChatMessageContext() :
           base("name=proiect")
          {
          }

          public virtual DbSet<ChatDBTable> Chats { get; set; }
     }
}
