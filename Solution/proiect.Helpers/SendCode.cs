using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Helpers
{
    public class SendCode
    {
        static public string SendCodeToUser(string strEmail) 
        {
            string codeRandom = GenerateRandomCode.GenerateRandom(8);
            SendEmail.SendEmailCode(strEmail, codeRandom);
            return codeRandom;
        }
    }
}
