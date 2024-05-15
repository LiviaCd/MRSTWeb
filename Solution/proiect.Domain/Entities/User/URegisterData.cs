using proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Domain.Entities
{
    public class URegisterData
    {
        public string Credential { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string LoginIp { get; set; }
        public DateTime LoginDateTime { get; set; }
        public string Email { get; set; }
        public URole Level { get; set; }
        public DateTime BlockTime { get; set; }
     }
}
