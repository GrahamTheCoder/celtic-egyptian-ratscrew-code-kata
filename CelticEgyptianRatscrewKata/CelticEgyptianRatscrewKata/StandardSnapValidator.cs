namespace CelticEgyptianRatscrewKata
{
    internal class StandardSnapValidator : ISnapValidator
    {
        public bool IsSnap(Stack stack)
        {
            Rank? lastRank = null;
            foreach (var card in stack)
            {
                if (lastRank.HasValue && lastRank.Value == card.Rank) return true;
                lastRank = card.Rank;
            }
            return false;
        }
    }
}