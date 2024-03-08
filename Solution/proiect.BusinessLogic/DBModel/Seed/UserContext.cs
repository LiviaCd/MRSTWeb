using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.DBModel.Seed
{
     public class UserContext : DbContext
    {
          public UserContext() :
               base("name=proiect")
          { 
          }

          public virtual DbSet<UDBTable> Users { get; set; }
     }
}
