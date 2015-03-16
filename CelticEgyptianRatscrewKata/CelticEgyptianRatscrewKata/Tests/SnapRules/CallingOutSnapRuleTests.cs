using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CelticEgyptianRatscrewKata.SnapRules;
using NSubstitute;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests.SnapRules
{
    [TestFixture]
    public class CallingOutSnapRuleTests
    {
        [Theory]
        public void CallingOutPlayedCardIsValidSnap(Rank rank, Suit suit)
        {
            var stack = new Cards(new List<Card>
            {
                new Card(suit, rank),
            });
            var tracker = Substitute.For<ICallingOutTracker>();
            tracker.LastRank.Returns(rank);
            var gameController = new CallingOutSnapRule(tracker);
            var canSnap = gameController.IsSnapValid(stack);
            Assert.That(canSnap, Is.True);
        }

        [Theory]
        public void CallingOutDifferentToPlayedCardIsNotValidSnap(Rank rank, Suit suit)
        {
            var differentRank = rank + 1;
            var stack = new Cards(new List<Card>
            {
                new Card(suit, differentRank),
            });
            var tracker = Substitute.For<ICallingOutTracker>();
            tracker.LastRank.Returns(rank);
            var gameController = new CallingOutSnapRule(tracker);
            var canSnap = gameController.IsSnapValid(stack);
            Assert.That(canSnap, Is.False);
        }
    }
}
