using CelticEgyptianRatscrewKata.Game;
using NSubstitute;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests.EndToEnd
{
    [TestFixture]
    public class EndToEndLoggedCallingOutTests
    {
        [Test]
        public void PlayingCardCallsOut()
        {
            var log = Substitute.For<ILog>();
            log.Log(Arg.Do<string>(IsCallingOutMessage));
            var game = new RatscrewGameFactory().Create(log);
            game.PlayCard(new Player("me"));
            Assert.Fail("The log should have mentioned calling out by now");
        }

        private void IsCallingOutMessage(string logMessage)
        {
            if (logMessage.ToUpperInvariant().Contains("CALL")) Assert.Pass("Calling out message: {0}", logMessage);
        }
    }
}