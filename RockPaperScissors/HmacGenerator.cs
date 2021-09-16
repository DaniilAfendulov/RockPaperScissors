using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class HmacGenerator
    {
        public byte[] GenerateHash(byte[] hmacKey, string input)
        {
            byte[] hash;
            var inputBytes = Encoding.Default.GetBytes(input);
            using (var hmac = new HMACSHA512(hmacKey))
            {
                hash = hmac.ComputeHash(inputBytes);
            }
            return hash;
        }
    }
}
