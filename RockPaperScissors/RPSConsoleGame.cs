using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class RPSConsoleGame : RPSGameWithPc
    {
        Command _help, _exit;
        Command[] _allCommands;
        public RPSConsoleGame(string[] args)
            : base(args)
        {
            _help = new Command("help", "?");
            _exit = new Command("exit", "0");
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

                Move userMove;
                if (TryFindMove(input, out userMove))
                {
                    isRightMove = true;
                    Println("Your move: " + userMove.Name);
                    Println("Computer move: " + compMove.Name);
                    var result = FindResult(userMove, compMove);
                    PrintResult(result);
                    continue;
                }

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



            } while (!isRightMove);

            Println($"HMAC key: {Convert.ToHexString(hmacKey)}");
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
        private string Read()
        {
            return Console.ReadLine();
        }
        private void Print(string msg)
        {
            Console.Write(msg);
        }


    }
}
