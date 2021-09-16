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
            var winRule = new WinRule(maxOrder);
            return winRule.FindClashResult(this, move);
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
