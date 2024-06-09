using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Domain.Entities.BloodR
{
     public class BloodResource
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          public string EmailClinic { get; set; }
          public int OnePozitive {get; set; }
          public int TwoPozitive { get; set; }
          public int ThirdPozitive { get; set; }
          public int FourPozitive { get; set; }
          public int OneNegative { get; set; }
          public int TwoNegative { get; set; }
          public int ThirdNegative { get; set; }
          public int FourNegative { get; set;}
          public bool Reservat {  get; set; }
          public string WhoReservate { get; set; }
     }
}
