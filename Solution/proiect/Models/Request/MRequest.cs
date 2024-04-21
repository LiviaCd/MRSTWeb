using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace proiect.Models.Request
{
     public class MRequest
     {    
          [Required]
          public string BloodType { get; set; }

          [StringLength(40)]
          public string City { get; set; }
     }
}