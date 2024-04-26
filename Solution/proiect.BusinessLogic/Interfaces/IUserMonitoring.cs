using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.Interfaces
{
    public interface IUserMonitoring
    {
        List<UserMinimal> GetAllUsers();
        ULoginResp AddNewUser(ANewUser addData);
        UserMinimal RGetUserById(int id);
        void EditUser(int id, UserMinimal user);

    }
}
