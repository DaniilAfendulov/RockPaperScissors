using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ConsoleTables;

namespace RockPaperScissors
{
    public class HelpTable
    {
        private ConsoleTable _table;
        public HelpTable(Move[] moves)
        {
            _table = new ConsoleTable(moves.Select(m => m.Name).Prepend("pc\\user").ToArray());
            foreach (var pcMove in moves)
            {
                var row = new List<string>();
                row.Add(pcMove.Name);
                row.AddRange(moves.Select(um => um.Clash(pcMove, moves.Length).ToString()));
                _table.AddRow(row.ToArray());
            }
            _table.Options.EnableCount = false;
        }

        public override string ToString()
        {
            return _table.ToString();
        }
    }
}
