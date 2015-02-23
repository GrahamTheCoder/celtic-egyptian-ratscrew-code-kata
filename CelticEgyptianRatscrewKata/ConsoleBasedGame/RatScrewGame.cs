using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;

namespace ConsoleBasedGame
{
    class RatScrewGame
    {
        public void PlayGame(GameController game, IGamePlayUserInterface userInterface)
        {
            game.StartGame(GameFactory.CreateFullDeckOfCards());

            char userInput;
            while (userInterface.TryReadUserInput(out userInput))
            {
            }
        }

        public GameController SetupGame(IGameSetupUserInterface userInterface)
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