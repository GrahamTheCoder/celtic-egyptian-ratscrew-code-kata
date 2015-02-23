using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;

namespace CelticEgyptianRatscrewKata.GameSetup
{
    public class RatScrewGame
    {
        private readonly IGameSetupUserInterface m_SetupInterface;
        private readonly IGamePlayUserInterface m_GamePlayInterface;
        private readonly IGameFactory m_GameFactory;
        private readonly IDictionary<char, Action<GameController>> m_PlayerActions = new Dictionary<char, Action<GameController>>();

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
                var player = new Player(playerInfo.PlayerName);
                m_GameFactory.AddPlayer(player);
                m_PlayerActions.Add(playerInfo.PlayCardKey, game => game.PlayCard(player));
                m_PlayerActions.Add(playerInfo.SnapKey, game => game.AttemptSnap(player));
            }
            return m_GameFactory.Create();
        }

        private void StartGame(GameController game, Cards cards)
        {
            game.StartGame(cards);

            char userInput;
            IPlayer winner;
            while (!game.TryGetWinner(out winner) && m_GamePlayInterface.TryReadUserInput(out userInput))
            {
                Action<GameController> action;
                if (m_PlayerActions.TryGetValue(userInput, out action))
                {
                    action(game);
                }
            }
        }
    }
}