using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;

namespace CelticEgyptianRatscrewKata.GameSetup
{
    public class RatScrewGame
    {
        private readonly IGameSetupUserInterface m_SetupInterface;
        private readonly IGamePlayUserInterface m_GamePlayInterface;
        private readonly IGameFactory m_GameFactory;

        public RatScrewGame(IGameSetupUserInterface setupInterface, IGamePlayUserInterface gamePlayInterface, IGameFactory gameFactory = null)
        {
            m_SetupInterface = setupInterface;
            m_GamePlayInterface = gamePlayInterface;
            m_GameFactory = gameFactory ?? new GameFactory();
        }

        public void Play()
        {
            var gameController = SetupGame();
            var cards = m_GameFactory.CreateFullDeckOfCards();
            StartGame(gameController, cards);
        }

        private GameController SetupGame()
        {
            IEnumerable<PlayerInfo> playerInfos = m_SetupInterface.GetPlayerInfoFromUserLazily();

            foreach (PlayerInfo playerInfo in playerInfos)
            {
                m_GameFactory.AddPlayer(new Player(playerInfo.PlayerName));
            }
            return m_GameFactory.Create();
        }

        private void StartGame(GameController game, Cards cards)
        {
            game.StartGame(cards);

            char userInput;
            while (m_GamePlayInterface.TryReadUserInput(out userInput))
            {
                game.PlayCard(new Player("Ali"));
            }
        }
    }
}