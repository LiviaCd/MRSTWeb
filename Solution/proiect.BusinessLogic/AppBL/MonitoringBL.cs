using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiect.Domain.Enums;
using proiect.Domain.Entities.News;

namespace proiect.BusinessLogic.AppBL
{
    public class MonitoringBL : AdminApi, IUserMonitoring
    {
        public List<UserMinimal> GetAllUsers()
        {
            return RGetAllUsers();
        }

        public UserMinimal GetUserById(int id)
        {
            return RGetUserById(id);
        }

        public void EditUser(int id, UserMinimal user)
        {
            REditUser(id, user);
        }
        public ULoginResp AddNewUser(ANewUser addData)
        {
            return AddNewUserAction(addData);
        }
          public void BlockUser1day(int id, UserMinimal userData)
          {
               RBlockUser1Day(id, userData);
          }

          public void BlockUser3day(int id, UserMinimal userData)
          {
               RBlockUser3Day(id, userData);
          }
          public void BlockUser30day(int id, UserMinimal userData)
          {
               RBlockUser30Day(id, userData);
          }
          public void BlockUserPermanent(int id, UserMinimal userData)
          {
               RBlockUserPermanent(id, userData);
          }
          public bool AddNewNews(AddNews news)
          {
               return RAddNewNews(news);
          }

          public AddNews GetNewsById(int Id)
          {
               return RGetNewsById(Id);
          }
          public void EditNewsAction(int id, AddNews news)
          {
               REditNewsAction(id, news);
          }

     }
}