namespace CelticEgyptianRatscrewKata.SnapRules
{
    public class CallingOutTracker : ICallingOutTracker
    {
        private Rank m_CalledOutRank = Rank.King;

        public Rank LastRank
        {
            get { return m_CalledOutRank; }
        }

        public void CalloutRank()
        {
            m_CalledOutRank = (m_CalledOutRank == Rank.King) ? Rank.Ace : m_CalledOutRank + 1;
        }
    }
}