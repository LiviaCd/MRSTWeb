using proiect.Domain.Entities.Session;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.DBModel.Seed
{
     public class SessionContext : DbContext
     {
          public SessionContext() :
             base ("name=proiect")
          {
          }

          public virtual DbSet<Session> Sessions { get; set; }
     }
}
