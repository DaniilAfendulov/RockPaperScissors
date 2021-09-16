using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class HmacKeyGenerator
    {
        public byte[] GenerateKey()
        {
            var hmacKey = new byte[128 / 8];
            var generator = RandomNumberGenerator.Create();
            generator.GetBytes(hmacKey);
            return hmacKey;
        }
    }
}
