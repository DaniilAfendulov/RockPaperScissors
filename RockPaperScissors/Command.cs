using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Command
    {
        public string Ch { get; }
        public string Name { get; }

        public Command(string name)
        {
            Name = name;
        }

        public Command(string name, string ch)
        {
            Name = name;
            Ch = ch;
        }

        public bool Is(string command)
        {
            return command == Ch || command == Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Command)
            {
                var cmd = obj as Command;
                return
                    Ch == cmd.Ch &&
                    Name == cmd.Name;
            }
            return false;
        }
    }
}
