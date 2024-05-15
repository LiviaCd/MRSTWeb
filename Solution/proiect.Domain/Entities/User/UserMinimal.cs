using proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Domain.Entities.User
{
     public class UserMinimal
     {
          public int Id { get; set; }
          public string UserName { get; set; }
          public string Password { get; set; }
          public string Email { get; set; }
          public string Address { get; set; }
          public string Phone { get; set; }
          public DateTime LastLogin { get; set; }
          public string LasIp { get; set; }
          public URole Level { get; set; }
          public DateTime BlockTime { get; set; }
     }
}
