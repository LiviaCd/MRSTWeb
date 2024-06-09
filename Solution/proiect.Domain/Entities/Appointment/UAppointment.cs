using proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Domain.Entities.Appointment
{
    public class UAppointment
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string BloodType { get; set; }
        public string Address { get; set; }

        public DateTime Date { get; set; }
          public UTimeHour Time { get; set; }
          public int IdUser { get; set; }
          public int AppointmentId { get; set; }
     }
}
