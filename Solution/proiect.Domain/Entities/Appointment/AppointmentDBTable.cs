using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiect.Domain.Enums;

namespace proiect.Domain.Entities.Appointment
{
    public class AppointmentDBTable
    {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int AppointmentId { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(9)]
        public string Phone { get; set; }

        [Required]
        public string BloodType { get; set; }
          [Required]
 
        public string Address { get; set; }
 
        [Required]
        public DateTime Date { get; set; }
          [Required]
          public UTimeHour Time { get; set; }
          public int IdUser { get; set; }
     }
}
