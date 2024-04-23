using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Helpers
{
    public class GenerateRandomCode
    {
        static public string GenerateRandom(int length)
        {
            // Define the characters to include in the code.
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var randomCode = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomCode[i] = chars[random.Next(chars.Length)];
            }

            return new String(randomCode);
        }
    }
}