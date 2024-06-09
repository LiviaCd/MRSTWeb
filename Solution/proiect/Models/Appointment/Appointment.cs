using proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace proiect.Models.Appointment
{
    public class Appointment
    {
          public int AppointmentId { get; set; }
          public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BloodType { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public UTimeHour Time {  get; set; }
          public int IdUser { get; set; }
     }
}