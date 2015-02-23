using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;

namespace CelticEgyptianRatscrewKata.GameSetup
{
    public class RatScrewGame
    {
        private readonly IGameSetupUserInterface m_SetupInterface;
        private readonly IGamePlayUserInterface m_GamePlayInterface;

        public RatScrewGame(IGameSetupUserInterface setupInterface, IGamePlayUserInterface gamePlayInterface)
        {
            m_SetupInterface = setupInterface;
            m_GamePlayInterface = gamePlayInterface;
        }

        public void Play()
        {
            var gameController = SetupGame();
            StartGame(gameController);
        }

        private GameController SetupGame()
        {
            var gameFactory = new GameFactory();

            IEnumerable<PlayerInfo> playerInfos = m_SetupInterface.GetPlayerInfoFromUserLazily();

            foreach (PlayerInfo playerInfo in playerInfos)
            {
                gameFactory.AddPlayer(new Player(playerInfo.PlayerName));
            }
            return gameFactory.Create();
        }

        private void StartGame(GameController game)
        {
            game.StartGame(GameFactory.CreateFullDeckOfCards());

            char userInput;
            while (m_GamePlayInterface.TryReadUserInput(out userInput))
            {

            }
        }
    }
}