using System;
namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var game = new RPSGame(args);
                game.Start();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine("The input example:");
                Console.WriteLine("rock paper scissors lizard Spock");
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot start the game");
            }

        }
      
    }
}
