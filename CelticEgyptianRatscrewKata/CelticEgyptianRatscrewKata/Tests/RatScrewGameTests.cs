using CelticEgyptianRatscrewKata.GameSetup;
using Moq;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    [TestFixture]
    class RatScrewGameTests
    {
        [Test]
        public void TerminatesWhenNoUserInputAvailable()
        {
            char userInput;
            var setupMock = new Mock<IGameSetupUserInterface>();
            setupMock.Setup(x => x.GetPlayerInfoFromUserLazily()).Returns(new[] {new PlayerInfo("Ali", '1', 'a'),});
            var gamePlayMock = new Mock<IGamePlayUserInterface>();
            gamePlayMock.Setup(x => x.TryReadUserInput(out userInput)).Returns(false);

            var game = new RatScrewGame(setupMock.Object, gamePlayMock.Object);
            game.Play();

            setupMock.Verify(x => x.GetPlayerInfoFromUserLazily(), Times.Once());
            gamePlayMock.Verify(x => x.TryReadUserInput(out userInput), Times.Once());

        }
    }
}
