using proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect.Models.User
{
    public class ModelNewUser
    {
        public string Credential { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public URole Level { get; set; }
    }
}