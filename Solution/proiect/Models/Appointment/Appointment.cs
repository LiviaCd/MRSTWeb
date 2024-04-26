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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BloodType { get; set; }
        public DateTime Data { get; set; }
        public DateTime Time { get; set; }
    }
}