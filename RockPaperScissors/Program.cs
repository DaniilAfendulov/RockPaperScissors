using System;
using System.Linq;
using System.Security.Cryptography;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!CheckInput(args))
            {
                Print("\nThe input example:");
                Print("rock paper scissors lizard Spock");
                return;
            }

            var help = new Command("?", "help");
            var exit = new Command("0", "exit");


            var generator = RandomNumberGenerator.Create();
            var hmacKey = "";
            Print($"HMAC: {hmacKey}");

            bool isRightMove;
            do
            {
                isRightMove = CheckMove();
                Console.Write("Enter your move: ");
                var playerMove = Console.ReadLine();
                if (exit.Is(playerMove))
                {
                    Print(exit.Name);
                    return;
                }
                if (help.Is(playerMove))
                {
                    PrintMoves();
                    isRightMove = false;
                    continue;
                }

                while (!CheckMove() || help.Is(playerMove))
                {
                    if (!CheckMove()) Print($"wrong command: {playerMove}");
                    if (help.Is(playerMove)) Print($"your command: {help.Name}");
                    PrintMoves();
                }


            } while (!isRightMove);

            Console.Write("Your move: ");


            var move = "";
            Print($"Computer move: : {move}");
            Print($"HMAC key: {hmacKey}");
        }

        static bool CheckInput(string[] input)
        {
            if (input == null || input.Length < 3)
            {
                Print("the number of input lines must be >= 3");
                return false;
            }
            if (input.Length % 2 == 0)
            {
                Print("the number of input lines must be odd");
                return false;
            }
            if (input.Distinct().Count() != input.Length)
            {
                Print("input has duplicate");
                return false;
            }

            return true;
        }

        static bool CheckMove()
        {
            return true;
        }

        static void PrintMoves()
        {
            Print("0 - exit");
            Print("? - help");
        }

        static void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
