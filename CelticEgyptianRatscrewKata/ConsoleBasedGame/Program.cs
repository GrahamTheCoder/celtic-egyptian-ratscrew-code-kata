using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;
using CelticEgyptianRatscrewKata.GameSetup;

namespace ConsoleBasedGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleInterface = new ConsoleInterface();
            var ratscrewGame = new RatScrewGame(consoleInterface, consoleInterface);
            ratscrewGame.Play();
        }
    }
}
