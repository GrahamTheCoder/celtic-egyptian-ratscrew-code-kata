using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    [TestFixture]
    public class StandardSnapValidatorTests
    {
        [Test]
        public void EmptyStackIsNotSnap()
        {
            var stack = CreateStack();
            var isSnap = IsSnap(stack);
            Assert.That(isSnap, Is.False);
        }

        [Test]
        public void SingleCardIsNotSnap()
        {
            var stack = CreateStack(new Card(Suit.Clubs, Rank.Ace));
            var isSnap = IsSnap(stack);
            Assert.That(isSnap, Is.False);
        }

        [Test]
        public void TwoEqualCardsIsSnap()
        {
            var card = new Card(Suit.Clubs, Rank.Ace);
            var stack = CreateStack(card, card);
            var isSnap = IsSnap(stack);
            Assert.That(isSnap, Is.True);
        }
        [Test]
        public void TwoEqualCardsAmongOthersIsSnap()
        {
            var card = new Card(Suit.Clubs, Rank.Two);
            var card2 = new Card(Suit.Clubs, Rank.Ace);
            var stack = CreateStack(card, card2, card2, card);
            var isSnap = IsSnap(stack);
            Assert.That(isSnap, Is.True);
        }

        private static Stack CreateStack(params Card[] cards)
        {
            return new Stack(cards);
        }

        private bool IsSnap(Stack stack)
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
