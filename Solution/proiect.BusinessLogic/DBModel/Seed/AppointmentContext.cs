using proiect.Domain.Entities.Ancheta;
using proiect.Domain.Entities.Appointment;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.DBModel.Seed
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext() :
           base("name=proiect")
        {
        }

        public virtual DbSet<AppointmentDBTable> Appointments { get; set; }
    }
}
