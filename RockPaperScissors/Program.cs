namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new RPSGame(args);
            game.Start();
        }
      
    }
}
