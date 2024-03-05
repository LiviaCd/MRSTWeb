using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiect.Domain.Entities.BloodType;

namespace proiect.BusinessLogic.Interfaces
{
     public interface IBloodType
     {
          BloodTypeDetail GetDetailBloodType(int id);
     }
}
