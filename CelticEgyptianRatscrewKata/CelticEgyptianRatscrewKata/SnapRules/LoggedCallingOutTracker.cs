namespace CelticEgyptianRatscrewKata.SnapRules
{
    public class LoggedCallingOutTracker : ICallingOutTracker
    {
        private ICallingOutTracker m_Child;

        public LoggedCallingOutTracker(ICallingOutTracker child, ILog log)
        {
            m_Log = log;
        }

        private ILog m_Log;

        public void CalloutRank()
        {
            m_Child.CalloutRank();
            m_Log.Log(string.Format("Calling out {0}", LastRank));
        }

        public Rank LastRank
        {
            get { return m_Child.LastRank; }
        }
    }
}