﻿using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}