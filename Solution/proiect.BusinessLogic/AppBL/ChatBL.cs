using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.ChatMessage;
using proiect.Domain.Entities.Profile;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.AppBL
{
     public class ChatBL : ChatAPI, IChatMessage
     {
          public void SendMessage (ChatDBTable text)
          {
               RSendMessage(text);
          }

          public List<ChatDBTable> DisplayAllChat ()
          {
               return RDisplayAllChat();
          }
          public List<UserMinimal> DisplayAllUser()
          {
               return RDisplayAllUser();
          }
          public UserMinimal GetUserByEmailChat(int email)
          {
               return RGetUserByEmailChat(email);
          }
          public void SendMessage(int id, UserViewChat model)
          {
               RSendMessage(id, model);
          }
     }
}
