using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.DBModel.Seed;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.BloodType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.AppBL
{
     public class BloodTypeBL : UserApi, IBloodType
     {

          public BloodTypeDetail GetDetailBloodType (int id)
          {
               return GetBloodTypeUser(id);
          }
     }
}
