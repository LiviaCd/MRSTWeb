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

namespace proiect.BusinessLogic.Interfaces
{
    public interface IUserMonitoring
    {
        List<UserMinimal> GetAllUsers();
        ULoginResp AddNewUser(ANewUser addData);
        UserMinimal RGetUserById(int id);
        void EditUser(int id, UserMinimal user);
        void BlockUser1day(int id, UserMinimal user);
        void BlockUser3day(int id, UserMinimal user);
        void BlockUser30day(int id, UserMinimal user);
        void BlockUserPermanent(int id, UserMinimal user);
        bool AddNewNews(AddNews news);
        AddNews RGetNewsById (int Id);
        void EditNewsAction(int id, AddNews news);

     }
}
