using proiect.Domain.Entities.Ancheta;
using proiect.Domain.Entities.Profile;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.DBModel.Seed
{
     public class ProfileContext : DbContext
     {
          public ProfileContext() :
            base("name=proiect")
          {
          }

          public virtual DbSet<UserProfileDBTable> PhotoProfile { get; set; }
     }
}
