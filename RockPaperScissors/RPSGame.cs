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
            if (!CheckInput(args, out var err))
            {
                err.ForEach(e => Println(e));
                Println();
                Println("The input example:");
                Println("rock paper scissors lizard Spock");
                return;
            }
            _help = new Command("help", "?");
            _exit = new Command("exit", "0");
            InitMoves(args);
            _allCommands = _moves.
                Select(m => (Command)m).
                Append(_help).
                Append(_exit).
                ToArray();
        }

        public RPSGame(Move[] moves)
        {
            _moves = moves;
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
                    Println(GetHelpTable());
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

        public static bool CheckInput(string[] input, out List<string> err)
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

        private void InitMoves(string[] args)
        {
            _moves = CreateMoves(args);
        }

        private void Print(string msg)
        {
            Console.Write(msg);
        }

        private void PrintCommands(Command[] commands)
        {
            Println(GetCommandsText(commands));
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

        private static Move CompMove(Move[] moves)
        {
            var number = RandomNumberGenerator.GetInt32(moves.Length);
            return moves[number];
        }

        private string Read()
        {
            return Console.ReadLine();
        }

        private static bool TryFindCommand(string input, Command[] commands, out Command command)
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
