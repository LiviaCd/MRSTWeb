using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect.Models.User
{
    public class UserRegister
    {
        public string Name { get; set; }
        public string Credential { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}