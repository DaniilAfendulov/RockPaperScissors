using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class WinRule
    {
        private int _maxOrder;
        public WinRule(int maxOrder)
        {
            _maxOrder = maxOrder;
        }
        /// <summary>
        /// Find the ClashResult for move1
        /// Example: if move1 win than result is Win
        /// Example: if move1 lose than result is Lose
        /// </summary>
        /// <returns>Win, Lose or Draw</returns>
        public ClashResult FindClashResult(Move move1, Move move2)
        {
            if (move2 == null) throw new ArgumentNullException("move parameter does not have a value");
            if (move2.Order == move1.Order) return ClashResult.Draw;
            var half = (_maxOrder - 1) / 2;
            if (move2.Order > move1.Order)
            {
                if (move2.Order - move1.Order <= half)
                {
                    return ClashResult.Lose;
                }
                return ClashResult.Win;
            }
            if (move1.Order - move2.Order <= half)
            {
                return ClashResult.Win;
            }
            return ClashResult.Lose;
        }
    }
}
