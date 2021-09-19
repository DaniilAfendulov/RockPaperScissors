using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class RPSGameWith2P : RPSGame
    {
        public Move Move1 { get; set; }
        public Move Move2 { get; set; }
        public RPSGameWith2P(string[] args)
            : base(args)
        {

        }

        public bool IsMovesSets()
        {
            return Move1 != null && Move2 != null;
        }

        public ClashResult FindResult()
        {
            if (!IsMovesSets())
            {
                throw new ArgumentException("move1 or move2 is null");
            }
            return _winRule.FindClashResult(Move1,Move2);
        }

    }
}
