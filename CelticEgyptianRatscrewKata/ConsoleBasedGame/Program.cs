using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;

namespace ConsoleBasedGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInterface = new ConsoleInterface();
            var ratscrewGame = new RatScrewGame();
            var gameController = ratscrewGame.SetupGame(userInterface);
            ratscrewGame.PlayGame(gameController, userInterface);
        }
    }
}
