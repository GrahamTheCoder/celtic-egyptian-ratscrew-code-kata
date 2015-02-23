using System.Collections.Generic;
using System.Linq;
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
            var playerInfos = CreatePlayers();
            var setupMock = MockGameSetupUserInterface(playerInfos);
            var gamePlayMock = new CannedResponseUserInterface(Enumerable.Empty<char>());

            var game = new RatScrewGame(setupMock.Object, gamePlayMock);
            game.Play();

            setupMock.Verify(x => x.GetPlayerInfoFromUserLazily(), Times.Once());
            Assert.That(gamePlayMock.TimesCalled, Is.EqualTo(1));
        }

        [Test]
        public void LayAllCards()
        {
            var playerInfos = CreatePlayers();
            var setupMock = MockGameSetupUserInterface(playerInfos);
            var gamePlayMock = new CannedResponseUserInterface(Enumerable.Repeat('1', 52));

            var game = new RatScrewGame(setupMock.Object, gamePlayMock);
            game.Play();
            
            Assert.That(gamePlayMock.TimesCalled, Is.EqualTo(53));
        }

        [Test]
        public void LayMoreThanAllCards()
        {
            var playerInfos = CreatePlayers();
            var setupMock = MockGameSetupUserInterface(playerInfos);
            var gamePlayMock = new CannedResponseUserInterface(Enumerable.Repeat('1', 53));

            var game = new RatScrewGame(setupMock.Object, gamePlayMock);
            Assert.That(game.Play, Throws.Exception);
        }

        private static Mock<IGameSetupUserInterface> MockGameSetupUserInterface(PlayerInfo[] playerInfos)
        {
            var setupMock = new Mock<IGameSetupUserInterface>();
            setupMock.Setup(x => x.GetPlayerInfoFromUserLazily()).Returns(playerInfos);
            return setupMock;
        }

        private static PlayerInfo[] CreatePlayers()
        {
            return new[] {new PlayerInfo("Ali", '1', 'a'),};
        }
    }


    internal class CannedResponseUserInterface : IGamePlayUserInterface
    {
        private readonly Queue<char> m_Characters;
        
        public CannedResponseUserInterface(IEnumerable<char> characters)
        {
            m_Characters = new Queue<char>(characters);
        }

        public bool TryReadUserInput(out char userInput)
        {
            TimesCalled++;
            var hasCharacters = m_Characters.Any();
            userInput = hasCharacters ? m_Characters.Dequeue() : default(char);
            return hasCharacters;
        }

        public int TimesCalled { get; private set; }
    }
}
