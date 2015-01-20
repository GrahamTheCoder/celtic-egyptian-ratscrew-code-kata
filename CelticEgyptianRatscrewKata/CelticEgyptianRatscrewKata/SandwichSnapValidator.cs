namespace CelticEgyptianRatscrewKata
{
    internal class SandwichSnapValidator : ISnapValidator
    {
        public bool IsSnap(Stack stack)
        {
            Rank? oneAgoRank = null;
            Rank? twoAgoRank = null;
            foreach (var card in stack)
            {
                if (twoAgoRank.HasValue && twoAgoRank.Value == card.Rank) return true;

                twoAgoRank = oneAgoRank;
                oneAgoRank = card.Rank;
            }
            return false;
        }
    }
}