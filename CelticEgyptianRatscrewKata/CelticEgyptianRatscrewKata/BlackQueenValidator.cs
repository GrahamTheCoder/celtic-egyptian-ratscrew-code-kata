using System;
using System.Linq;

namespace CelticEgyptianRatscrewKata
{
    internal class BlackQueenValidator : ISnapValidator
    {
        public bool IsSnap(Stack stack)
        {
            var last = stack.LastOrDefault();
            if (last == null) return false;
            return last.Rank == Rank.Queen && last.Suit == Suit.Spades;
        }
    }
}