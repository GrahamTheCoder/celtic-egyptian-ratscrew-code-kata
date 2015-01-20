using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    [TestFixture]
    public class BlackQueenValidatorTests
    {
        private readonly ISnapValidator m_SnapValidator = new BlackQueenValidator();

        [Test]
        public void EmptyStackIsNotSnap()
        {
            var stack = CreateStack();
            var isSnap = m_SnapValidator.IsSnap(stack);
            Assert.That(isSnap, Is.False);
        }

        [Test]
        public void QueenOfSpadesOnTopIsSnap()
        {
            var stack = CreateStack(new Card(Suit.Spades, Rank.Queen));
            var isSnap = m_SnapValidator.IsSnap(stack);
            Assert.That(isSnap, Is.True);
        }

        [Test]
        public void QueenOfClubsOnTopIsNotSnap()
        {
            var stack = CreateStack(new Card(Suit.Clubs, Rank.Queen));
            var isSnap = m_SnapValidator.IsSnap(stack);
            Assert.That(isSnap, Is.False);
        }

        private static Stack CreateStack(params Card[] cards)
        {
            return new Stack(cards);
        }

    }
}
