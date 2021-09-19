using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class RPSGameWithPc : RPSGame
    {
        public RPSGameWithPc(string[] args)
            : base(args)
        {

        }

        protected static Move CompMove(Move[] moves)
        {
            var number = RandomNumberGenerator.GetInt32(moves.Length);
            return moves[number];
        }


        protected byte[] GenerateHash(byte[] hmacKey, string input)
        {
            return new HmacGenerator().GenerateHash(hmacKey, input);
        }

        protected byte[] GenerateHmacKey()
        {
            return new HmacKeyGenerator().GenerateKey();
        }
    }
}
