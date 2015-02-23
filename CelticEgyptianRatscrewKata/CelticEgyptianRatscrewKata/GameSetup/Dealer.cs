using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace CelticEgyptianRatscrewKata.GameSetup
{
    public class Dealer : IDealer
    {
        public List<Cards> Deal(uint numberOfHands, Cards deck)
        {
            if (numberOfHands <= 0) throw new NotSupportedException("Can only deal to one or more players");

            var hands = new List<Cards>();

            for (int i = 0; i < numberOfHands; i++)
            {
                hands.Add(Cards.Empty());
            }

            while (deck.HasCards)
            {
                for (int i = 0; i < numberOfHands && deck.HasCards; i++)
                {
                    hands.ElementAt(i).AddToTop(deck.Pop());
                }
            }

            return hands;
        }
    }
}