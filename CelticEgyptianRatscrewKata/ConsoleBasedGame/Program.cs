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

        private static void PlayGame(GameFactory gameFactory, ConsoleInterface userInterface)
        {
            GameController game = gameFactory.Create();
            game.StartGame(GameFactory.CreateFullDeckOfCards());

            char userInput;
            while (userInterface.TryReadUserInput(out userInput))
            {
            }
        }

        private static GameFactory SetupGame(ConsoleInterface userInterface)
        {
            var gameFactory = new GameFactory();

            IEnumerable<PlayerInfo> playerInfos = userInterface.GetPlayerInfoFromUserLazily();

            foreach (PlayerInfo playerInfo in playerInfos)
            {
                gameFactory.AddPlayer(new Player(playerInfo.PlayerName));
            }
            return gameFactory;
        }
    }
}
