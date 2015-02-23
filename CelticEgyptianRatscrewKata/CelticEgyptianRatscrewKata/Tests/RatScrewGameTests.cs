using System.Collections.Generic;
using System.Linq;
using CelticEgyptianRatscrewKata.Game;
using CelticEgyptianRatscrewKata.GameSetup;
using Moq;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    [TestFixture]
    class RatScrewGameTests
    {
        private static readonly Card s_AceOfSpades = new Card(Suit.Spades, Rank.Ace);

        [Test]
        public void TerminatesWhenNoUserInputAvailable()
        {
            var playerInfos = CreatePlayers(2);
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
            var playerInfos = CreatePlayers(2);
            var setupMock = MockGameSetupUserInterface(playerInfos);
            var gamePlayMock = new CannedResponseUserInterface(Enumerable.Repeat(playerInfos.First().SnapKey, 52));

            var game = new RatScrewGame(setupMock.Object, gamePlayMock);
            game.Play();
            
            Assert.That(gamePlayMock.TimesCalled, Is.EqualTo(53));
        }

        [Test]
        public void SinglePlayerHasWon()
        {
            var playerInfos = CreatePlayers(1);
            var setupMock = MockGameSetupUserInterface(playerInfos);
            var gamePlayMock = new CannedResponseUserInterface(Enumerable.Empty<char>());
            var game = new RatScrewGame(setupMock.Object, gamePlayMock);
            game.Play();
            Assert.That(gamePlayMock.TimesCalled, Is.EqualTo(0));
        }


        [Test]
        public void SnapCards()
        {
            var playerInfos = CreatePlayers(2);
            var setupMock = MockGameSetupUserInterface(playerInfos);
            var gamePlayMock = new CannedResponseUserInterface(Enumerable.Repeat('1', 53));
            var gameFactory = new Mock<IGameFactory>();
            gameFactory.Setup(x => x.CreateFullDeckOfCards()).Returns(Cards(s_AceOfSpades, s_AceOfSpades));
            var game = new RatScrewGame(setupMock.Object, gamePlayMock, gameFactory.Object);
            Assert.That(game.Play, Throws.Exception);
        }

        private static Cards Cards(params Card[] cards)
        {
            return new Cards(cards);
        }

        private static Mock<IGameSetupUserInterface> MockGameSetupUserInterface(IEnumerable<PlayerInfo> playerInfos)
        {
            var setupMock = new Mock<IGameSetupUserInterface>();
            setupMock.Setup(x => x.GetPlayerInfoFromUserLazily()).Returns(playerInfos);
            return setupMock;
        }

        private static IList<PlayerInfo> CreatePlayers(int numberOfPlayers)
        {
            return Enumerable.Range(1, numberOfPlayers).Select(CreatePlayer).ToList();
        }

        private static PlayerInfo CreatePlayer(int i)
        {
            return new PlayerInfo("name" + i, (char) ('a' + i), (char) ('1' + i));
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
