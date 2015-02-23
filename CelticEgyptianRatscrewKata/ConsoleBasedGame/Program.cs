using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;

namespace ConsoleBasedGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameFactory = new GameFactory();


            var userInterface = new ConsoleInterface();
            IEnumerable<PlayerInfo> playerInfos = userInterface.GetPlayerInfoFromUserLazily();

            foreach (PlayerInfo playerInfo in playerInfos)
            {
                gameFactory.AddPlayer(new Player(playerInfo.PlayerName));
            }


            GameController game = gameFactory.Create();
            game.StartGame(GameFactory.CreateFullDeckOfCards());

            char userInput;
            while (userInterface.TryReadUserInput(out userInput))
            {

            } 
        }
    }
}
