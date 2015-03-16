using System.Linq;

namespace CelticEgyptianRatscrewKata.SnapRules
{
    public class CallingOutSnapRule : ISnapRule
    {
        private ICallingOutTracker m_Tracker;

        public CallingOutSnapRule(ICallingOutTracker tracker)
        {
            m_Tracker = tracker;
        }
        public bool IsSnapValid(Cards cardStack)
        {
            var topOfStack = cardStack.FirstOrDefault();
            return topOfStack != null && topOfStack.Rank == m_Tracker.LastRank;
        }
    }
}