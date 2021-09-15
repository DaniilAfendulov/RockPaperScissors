using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace RockPaperScissors
{
    public class RPSGame
    {
        Command _help, _exit;
        Move[] _moves;
        Command[] _allCommands;

        public RPSGame(string[] args)
        {
            if (!CheckInput(args))
            {
                Print("\nThe input example:");
                Print("rock paper scissors lizard Spock");
                return;
            }

            _help = new Command("?", "help");
            _exit = new Command("0", "exit");
            _moves = CreateMoves(args);
            var allCommands = new List<Command>();
            allCommands.AddRange(_moves);
            allCommands.Add(_help);
            allCommands.Add(_exit);
            _allCommands = allCommands.ToArray();
        }

        public void Start()
        {
            var generator = RandomNumberGenerator.Create();
            var hmacKey = "";
            Print($"HMAC: {hmacKey}");

            bool isRightMove = false;
            do
            {
                PrintCommands(_allCommands);

            } while (!isRightMove);

            Console.Write("Your move: ");


            var move = "";
            Print($"Computer move: : {move}");
            Print($"HMAC key: {hmacKey}");
        }

        private bool CheckInput(string[] input)
        {
            var isRight = true;
            if (input == null || input.Length < 3)
            {
                Print("the number of input lines must be >= 3");
                isRight = false;
            }
            if (input.Length % 2 == 0)
            {
                Print("the number of input lines must be odd");
                isRight = false;
            }
            if (input.Distinct().Count() != input.Length)
            {
                Print("input has duplicate");
                isRight = false;
            }
            return isRight;
        }


        private Move[] CreateMoves(string[] args)
        {
            var moves = new List<Move>();
            for (int i = 0; i < args.Length; i++)
            {
                moves.Add(new Move(args[i], i.ToString(), i));
            }
            return moves.ToArray();
        }

        private void PrintCommands(Command[] commands)
        {
            foreach (var cmd in commands)
            {
                Print($"{cmd.Ch} - {cmd.Name}");
            }
        }

        private void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
