using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace proiect.Domain.Entities.Ancheta
{
     public class Ancheta
     {
               [Key]
               [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
               public int AnchetaId { get; set; }

               [Required]
               [StringLength(30)]
               public string FirstName { get; set; }

               [Required]
               [StringLength(30)]
               public string LastName { get; set; }

               [Required]
               public int Age { get; set; }

               [Required]
               public string Gender { get; set; }

               [Required]
               public string BloodType { get; set; }
               
               [Required]
               [StringLength(40)]
               public string Email { get; set; }

               [Required]
               [StringLength(40)]
               public string City { get; set; }


               [StringLength(40)]
               public string CityRural { get; set; }

               [StringLength(40)]
               public string District { get; set; }

               [Required]
               [StringLength(9)]
               public string Phone { get; set; }
          }
}