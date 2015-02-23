using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;

namespace ConsoleBasedGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInterface = new ConsoleInterface();
            var gameFactory = SetupGame(userInterface);
            PlayGame(gameFactory, userInterface);
        }

        private static void PlayGame(GameController game, IGamePlayUserInterface userInterface)
        {
            game.StartGame(GameFactory.CreateFullDeckOfCards());

            char userInput;
            while (userInterface.TryReadUserInput(out userInput))
            {
            }
        }

        private static GameController SetupGame(IGameSetupUserInterface userInterface)
        {
            var gameFactory = new GameFactory();

            IEnumerable<PlayerInfo> playerInfos = userInterface.GetPlayerInfoFromUserLazily();

            foreach (PlayerInfo playerInfo in playerInfos)
            {
                gameFactory.AddPlayer(new Player(playerInfo.PlayerName));
            }
            return gameFactory.Create();
        }
    }
}
