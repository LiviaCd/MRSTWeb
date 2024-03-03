using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;


namespace proiect.BusinessLogic.Core
{
     public class UserApi
     {
          public ULoginResp RLoginUpService(ULoginData data)
          {
               //Step 1 - SELECT FROM DB>Users WHERE data.
               // PASSWORD == data.Password

               //Step 2 - IF object != NULL
               // CREATE SESSION

               //RETURN SESSION AND STATUS TRUE

               return new ULoginResp { Status = false };
          }

     }
}
