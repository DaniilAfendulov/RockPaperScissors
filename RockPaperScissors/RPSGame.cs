using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ConsoleTables;

namespace RockPaperScissors
{
    public class RPSGame
    {
        protected Move[] _moves;
        protected WinRule _winRule;
        
        public RPSGame(string[] args)
        {
            if (!CheckInput(args, out var err))
            {
                throw new ArgumentException(string.Join('\n',err));
            }
            InitMoves(args);
            _winRule = new WinRule(_moves.Length);
        }

        public RPSGame(Move[] moves)
        {
            _moves = moves;
        }


        public static bool CheckInput(string[] input, out List<string> err, int ArgsNumberLimit=0)
        {
            err = new List<string>();
            var isRight = true;
            if (input == null || input.Length < 3)
            {
                err.Add("the number of input lines must be >= 3");
                isRight = false;
            }
            if (input.Length % 2 == 0)
            {
                err.Add("the number of input lines must be odd");
                isRight = false;
            }
            if (input.Distinct().Count() != input.Length)
            {
                err.Add("input has duplicate");
                isRight = false;
            }
            if (ArgsNumberLimit==0 && input.Length > ArgsNumberLimit)
            {
                err.Add("the number of input lines more than ArgsNumberLimit");
                isRight = false;
            }
            return isRight;
        }

        public static string GetCommandsText(Command[] commands)
        {
            var result = new StringBuilder();
            foreach (var cmd in commands)
            {
                result.Append($"{cmd.Ch} - {cmd.Name}");
            }
            return result.ToString();
        }

        public string GetCommandsText()
        {
            return GetCommandsText(_moves);
        }

        public static string GetHelpTable(Move[] moves)
        {
            var table = new HelpTable(moves);
            return table.ToString();
        }
        public string GetHelpTable()
        {
            return GetHelpTable(_moves);
        }

        public static Move[] CreateMoves(string[] args)
        {
            var moves = new List<Move>();
            for (int i = 0; i < args.Length; i++)
            {
                moves.Add(new Move(args[i], $"{i + 1}", i+1));
            }
            return moves.ToArray();
        }
        public void InitMoves(string[] args)
        {
            _moves = CreateMoves(args);
        }

        public static bool TryFindCommand(string input, Command[] commands, out Command command)
        {
            bool isFind = false;
            var chCmds = commands.ToDictionary(c => c.Ch);
            isFind = chCmds.TryGetValue(input, out command);
            if (isFind) return true;

            chCmds = commands.ToDictionary(c => c.Name.ToLower());
            isFind = chCmds.TryGetValue(input.ToLower(), out command);
            return isFind;
        }

        public static bool TryFindMove(string input, Move[] moves, out Move move)
        {
            Command command;
            if (TryFindCommand(
                input,
                moves.Select(m => (Command)m).ToArray(),
                out command))
            {
                if (command is Move)
                {
                    move = command as Move;
                    return true;
                }
            }
            move = null;
            return false;

        }
        public bool TryFindMove(string input, out Move move)
        {
            return TryFindMove(input, _moves, out move);
        }

        public ClashResult FindResult(Move move1, Move move2)
        {
            return _winRule.FindClashResult(move1, move2);
        }

    }
}
