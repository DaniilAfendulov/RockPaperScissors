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
        Command _help, _exit;
        Move[] _moves;
        Command[] _allCommands;
        
        public RPSGame(string[] args)
        {
            if (!CheckInput(args))
            {
                Println();
                Println("The input example:");
                Println("rock paper scissors lizard Spock");
            }
            _help = new Command("help", "?");
            _exit = new Command("exit", "0");
            _moves = CreateMoves(args);
            _allCommands = _moves.
                Select(m => (Command)m).
                Append(_help).
                Append(_exit).
                ToArray();
        }

        public void Start()
        {
            var compMove = CompMove(_moves);
            var hmacKey = GenerateHmacKey();
            var hash = GenerateHash(hmacKey, compMove.Name);
            Println($"HMAC: {Convert.ToHexString(hash)}");



            bool isRightMove = false;
            do
            {
                PrintCommands(_allCommands);
                Println();

                Print("Enter your move: ");
                var input = Read();

                Command userCmd;
                bool isCmdFind = TryFindCommand(input, _allCommands, out userCmd);

                if (!isCmdFind)
                {
                    isRightMove = false;
                    continue;
                }

                if (userCmd == _exit)
                {
                    PrintEnteredCommand(userCmd);
                    return;
                }

                if (userCmd == _help)
                {
                    PrintEnteredCommand(userCmd);
                    PrintHelpTable(_moves);
                    continue;
                }

                if (userCmd is Move)
                {
                    isRightMove = true;
                    var userMove = userCmd as Move;
                    Println("Your move: " + userCmd.Name);
                    Println("Computer move: " + compMove.Name);

                    var result = userMove.Clash(compMove, _moves.Length+1);
                    PrintResult(result);                    
                }

            } while (!isRightMove);

            Println($"HMAC key: {Convert.ToHexString(hmacKey)}");
        }

        private bool CheckInput(string[] input)
        {
            var isRight = true;
            if (input == null || input.Length < 3)
            {
                Println("the number of input lines must be >= 3");
                isRight = false;
            }
            if (input.Length % 2 == 0)
            {
                Println("the number of input lines must be odd");
                isRight = false;
            }
            if (input.Distinct().Count() != input.Length)
            {
                Println("input has duplicate");
                isRight = false;
            }
            return isRight;
        }

        private Move[] CreateMoves(string[] args)
        {
            var moves = new List<Move>();
            for (int i = 0; i < args.Length; i++)
            {
                moves.Add(new Move(args[i], $"{i + 1}", i+1));
            }
            return moves.ToArray();
        }

        private void Print(string msg)
        {
            Console.Write(msg);
        }

        private void PrintCommands(Command[] commands)
        {
            foreach (var cmd in commands)
            {
                Println($"{cmd.Ch} - {cmd.Name}");
            }
        }

        private void Println(string msg)
        {
            Print(msg + Environment.NewLine);
        }

        private void Println() => Println("");

        private void PrintHelpTable(Move[] moves)
        {
            var table = new HelpTable(moves);
            Println(table.ToString());
        }

        private void PrintResult(ClashResult result)
        {
            Print("Result: ");
            switch (result)
            {
                case ClashResult.Win:
                case ClashResult.Lose:
                    Print("You ");
                    break;
            }
            Println(result.ToString());
        }

        private void PrintEnteredCommand(Command command)
        {
            Println("Your command: " + command.Name);
        }

        private Move CompMove(Move[] moves)
        {
            var number = RandomNumberGenerator.GetInt32(moves.Length);
            return moves[number];
        }

        private string Read()
        {
            return Console.ReadLine();
        }

        private bool TryFindCommand(string input, Command[] commands, out Command command)
        {
            bool isFind = false;
            var chCmds = commands.ToDictionary(c => c.Ch);
            isFind = chCmds.TryGetValue(input, out command);
            if (isFind) return true;

            chCmds = commands.ToDictionary(c => c.Name.ToLower());
            isFind = chCmds.TryGetValue(input.ToLower(), out command);
            return isFind;
        }

        private byte[] GenerateHash(byte[] hmacKey, string input)
        {
            return new HmacGenerator().GenerateHash(hmacKey, input);
        }

        private byte[] GenerateHmacKey()
        {
            return new HmacKeyGenerator().GenerateKey();
        }
    }
}
