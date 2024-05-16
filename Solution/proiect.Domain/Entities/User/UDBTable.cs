using proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Domain.Entities.User
{
     public class UDBTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }
          [Required]
          [Display(Name = "Email Address")]
          [StringLength(30)]
          public string Email { get; set; }

          [Required]
          [Display(Name = "First Name")]
          [StringLength(30)]
          public string FirstName { get; set; }
          
          [Required]
          [Display(Name = "Last Name")]
          [StringLength(30)]
          public string LastName { get; set; }

          [Required]
          [Display(Name = "Password")]
          [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 characters")]
          public string Password { get; set; }
          
          
          [Required]
          [Display(Name = "Address")]
          [StringLength(50)]
          public string Address { get; set; }
          [Required]
          [Display(Name = "Phone")]
          [StringLength(9)]
          public string Phone { get; set; }

          [DataType(DataType.Date)]
          public DateTime LastLogin { get; set; }

          [StringLength(30)]
          public string LasIp { get; set; }

          public URole Level { get; set; }
          
          public DateTime BlockTime { get; set; }
     }
}
