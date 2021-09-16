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
            catch (System.Exception)
            {
                System.Console.WriteLine("Cannot start the game");
            }

        }
      
    }
}
