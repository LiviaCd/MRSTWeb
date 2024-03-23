using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Domain.Enums;
using proiect.Domain.Entities.BloodType;
using proiect.BusinessLogic.DBModel.Seed;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using proiect.Domain.Entities;


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


               //using (var db = new UserContext() )
              // {
               //     UDBTable user = db.Users.FirstOrDefault(us => us.UserName == data.Credential);
            //   }
               
               return new ULoginResp { Status = false };
          }
          internal ULoginResp RRegisterNewUserAction(URegisterData data)
          {
               var newUser = new UDBTable
               {
                    UserName = data.UserName,
                    Password = data.Password,
                    LasIp = "",
                    LastLogin = DateTime.Now,
                    Level = URole.User
               };

               using (var db = new UserContext())
               {
                    db.Users.Add(newUser);
                    db.SaveChanges();
               }
               return new ULoginResp();
          }
          public BloodTypeDetail GetBloodTypeUser (int id)
          {
               return new BloodTypeDetail();
          }

     }
}
