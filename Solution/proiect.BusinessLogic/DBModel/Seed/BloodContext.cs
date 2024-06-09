using proiect.Domain.Entities.Appointment;
using proiect.Domain.Entities.BloodR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.DBModel.Seed
{
     public class BloodContext : DbContext
     {
          public BloodContext() :
           base("name=proiect")
          {
          }

          public virtual DbSet<BloodResource> Resources { get; set; }
     }
}
