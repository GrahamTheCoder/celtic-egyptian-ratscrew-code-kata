using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;

namespace CelticEgyptianRatscrewKata.GameSetup
{
    public class RatScrewGame
    {
        private readonly IGameSetupUserInterface m_SetupInterface;
        private readonly IGamePlayUserInterface m_GamePlayInterface;
        private readonly GameFactory m_GameFactory;

        public RatScrewGame(IGameSetupUserInterface setupInterface, IGamePlayUserInterface gamePlayInterface)
        {
            m_SetupInterface = setupInterface;
            m_GamePlayInterface = gamePlayInterface;
            m_GameFactory = new GameFactory();
        }

        public void Play()
        {
            var gameController = SetupGame(m_GameFactory);
            var cards = m_GameFactory.CreateFullDeckOfCards();
            StartGame(gameController, cards);
        }

        private GameController SetupGame(GameFactory factory)
        {
            IEnumerable<PlayerInfo> playerInfos = m_SetupInterface.GetPlayerInfoFromUserLazily();

            foreach (PlayerInfo playerInfo in playerInfos)
            {
                factory.AddPlayer(new Player(playerInfo.PlayerName));
            }
            return factory.Create();
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