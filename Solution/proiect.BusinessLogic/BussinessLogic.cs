﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiect.BusinessLogic.Core;
using proiect.BusinessLogic.Interfaces;
using proiect.BusinessLogic;

namespace proiect.BusinessLogic
{
     public class BussinessLogic
     {
          public ISession GetSessionBL()
          {
               return new SessionBL();
          }

          public IBloodType GetBloodTypeBL()
          {
               return new BloodTypeBL();
          }

     }
}
