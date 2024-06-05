using proiect.Domain.Entities.ChatMessage;
using proiect.Domain.Entities.Profile;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.Interfaces
{
     public interface IChatMessage
     {
          void SendMessage (ChatDBTable chat);
          List<ChatDBTable> DisplayAllChat();
          List<UserMinimal> DisplayAllUser();
          UserMinimal GetUserByEmailChat(int id);
          void SendMessage(int id, UserViewChat model);
     }
}
