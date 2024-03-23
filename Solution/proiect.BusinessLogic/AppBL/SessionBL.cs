using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.DBModel.Seed;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;


namespace proiect.BusinessLogic
{
     public class SessionBL : UserApi, ISession
     {
          public ULoginResp UserLoginAction(ULoginData data)
          {
               UDBTable user;

               using (var db = new UserContext())
               {
                    user = db.Users.FirstOrDefault(us => us.UserName == data.Credential);
               }
               return RLoginUpService(data);
          }
          public ULoginResp RegisterNewUserAction(URegisterData regData)
          {
               return RRegisterNewUserAction(regData);
          }


     }
}
