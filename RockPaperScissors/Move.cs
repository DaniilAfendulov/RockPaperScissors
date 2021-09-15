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
            if(move == null) throw new ArgumentNullException("move parameter does not have a value");
            if (move.Order == Order) return ClashResult.Draw;
            var half = (maxOrder - 1) / 2;
            if (move.Order > Order)
            {
                if (move.Order - Order <= half)
                {
                    return ClashResult.Lose;
                }
                return ClashResult.Win;
            }
            if (Order - move.Order <= half)
            {
                return ClashResult.Win;
            }
            return ClashResult.Lose;
            
        }

        public override bool Equals(object obj)
        {
            if (obj is Move && base.Equals(obj))
            {
                var move = obj as Move;
                return Order == move.Order;
            }
            return false;
        }
    }
}
