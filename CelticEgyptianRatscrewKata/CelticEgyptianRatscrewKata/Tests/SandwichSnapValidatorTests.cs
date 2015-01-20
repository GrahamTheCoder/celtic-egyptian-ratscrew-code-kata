using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    [TestFixture]
    public class SandwichSnapValidatorTests
    {
        private readonly ISnapValidator m_SnapValidator = new SandwichSnapValidator();

        [Test]
        public void EmptyStackIsNotSnap()
        {
            var stack = CreateStack();
            var isSnap = m_SnapValidator.IsSnap(stack);
            Assert.That(isSnap, Is.False);
        }

        [Test]
        public void SameSuitSandwichIsASnap()
        {
            var card1 = new Card(Suit.Clubs, Rank.Two);
            var card2 = new Card(Suit.Clubs, Rank.Ace);

            var stack = CreateStack(card1, card2, card1);
            var isSnap = m_SnapValidator.IsSnap(stack);
            Assert.That(isSnap, Is.True);
        }

        [Test]
        public void DifferentSuitSandwichIsASnap()
        {
            var card1 = new Card(Suit.Clubs, Rank.Two);
            var breadInClubs = new Card(Suit.Clubs, Rank.Ace);
            var breadInSpades = new Card(Suit.Spades, Rank.Ace);

            var stack = CreateStack(breadInSpades, card1, breadInClubs);
            var isSnap = m_SnapValidator.IsSnap(stack);
            Assert.That(isSnap, Is.True);
        }

        private static Stack CreateStack(params Card[] cards)
        {
            return new Stack(cards);
        }
    }
}
