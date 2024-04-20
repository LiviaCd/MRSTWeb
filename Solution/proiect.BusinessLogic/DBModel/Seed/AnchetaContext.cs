using proiect.Domain.Entities.Ancheta;
using proiect.Domain.Entities.Session;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.DBModel.Seed
{
     public class AnchetaContext : DbContext
     {
          public AnchetaContext() :
             base("name=proiect")
          {
          }

          public virtual DbSet<Ancheta> Anchete { get; set; }
     }
}
