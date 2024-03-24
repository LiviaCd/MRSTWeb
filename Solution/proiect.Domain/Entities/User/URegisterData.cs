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
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public string LoginIp { get; set; }
        public DateTime LoginDateTime { get; set; }
    }
}
