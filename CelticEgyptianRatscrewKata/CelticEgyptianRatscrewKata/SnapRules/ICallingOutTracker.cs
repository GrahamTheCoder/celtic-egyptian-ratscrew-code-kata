namespace CelticEgyptianRatscrewKata.SnapRules
{
    public interface ICallingOutTracker
    {
        void CalloutRank();
        Rank LastRank { get; }
    }
}