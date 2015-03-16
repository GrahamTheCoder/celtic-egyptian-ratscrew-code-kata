using System.Collections.Generic;
using CelticEgyptianRatscrewKata.SnapRules;
using CelticEgyptianRatscrewKata.Tests.SnapRules;

namespace CelticEgyptianRatscrewKata.Game
{
    public class CallingOutSnapGameController : IGameController
    {

        private readonly ILog m_Log;
        private readonly IGameController m_GameController;
        private readonly ICallingOutTracker m_CallingOutTracker;

        public CallingOutSnapGameController(IGameController gameController, ICallingOutTracker callingOutTracker)
        {
            m_GameController = gameController;
            m_CallingOutTracker = callingOutTracker;
        }

        public bool AddPlayer(IPlayer player)
        {
            return m_GameController.AddPlayer(player);
        }

        public Card PlayCard(IPlayer player)
        {
            m_CallingOutTracker.CalloutRank();
            return m_GameController.PlayCard(player);
        }

        public bool AttemptSnap(IPlayer player)
        {
            return m_GameController.AttemptSnap(player);
        }

        public void StartGame(Cards deck)
        {
            m_GameController.StartGame(deck);
        }

        public bool TryGetWinner(out IPlayer winner)
        {
            return m_GameController.TryGetWinner(out winner);
        }

        public IEnumerable<IPlayer> Players
        {
            get { return m_GameController.Players; }
        }

        public int StackSize
        {
            get { return m_GameController.StackSize; }
        }

        public Card TopOfStack
        {
            get { return m_GameController.TopOfStack; }
        }

        public int NumberOfCards(IPlayer player)
        {
            return m_GameController.NumberOfCards(player);
        }
    }
}