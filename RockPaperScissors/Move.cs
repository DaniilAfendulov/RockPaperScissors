using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Move : Command
    {
        public int Order { get; }
        public Move(string name, string ch, int order) : base(name,ch)
        {
            Order = order;
        }

        public ClashResult Clash(Move move, int maxOrder)
        {
            if (move.Order == Order) return ClashResult.Draw;
            if (move.Order > Order) return ClashResult.Lose;
            if (move.Order < Order) return ClashResult.Win;
            throw new ArgumentNullException("move parameter does not have a value");
        }
    }
}
