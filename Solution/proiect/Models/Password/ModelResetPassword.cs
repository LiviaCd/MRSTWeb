using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect.Models.Password
{
    public class ModelResetPassword
    {
        public string Email { get; set; }
        public string CodeSend { get; set; }
        public string CodeWrite { get; set; }
    }
}