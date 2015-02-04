using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CelticEgyptianRatscrewKata.SnapRules;
using Moq;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void GameHasNoWinnerIfWinStateCheckerReturnsNull()
        {
            var gameBuilder = new GameBuilder();
            var alice = new Player("alice");
            gameBuilder.AddPlayer(alice);
            var winChecker = new Mock<IWinStateChecker>();
            winChecker.Setup(x => x.GetWinningPlayer(It.IsAny<Game>())).Returns((Player?) null);
            var game = gameBuilder.Build(winChecker.Object, Cards.Empty());
            var winner = game.Winner;
            Assert.That(winner, Is.EqualTo(null));
        }

        [Test]
        public void GameHasWinnerIfWinStateCheckerReturnsWinner()
        {
            var gameBuilder = new GameBuilder();
            var alice = new Player("alice");
            gameBuilder.AddPlayer(alice);
            var winChecker = new Mock<IWinStateChecker>();
            winChecker.Setup(x => x.GetWinningPlayer(It.IsAny<Game>())).Returns(alice);
            var game = gameBuilder.Build(winChecker.Object, Cards.Empty());
            var winner = game.Winner;
            Assert.That(winner, Is.EqualTo(alice));
        }

        [Test]
        public void PlayerCannotPlayCardsIfTheyHaveNone()
        {
            var gameBuilder = new GameBuilder();
            Player alice = new Player("alice");
            gameBuilder.AddPlayer(alice);
            var game = gameBuilder.Build(new WinStateChecker(), Cards.With(new Card(Suit.Spades, Rank.Ace)));

            game.PlayCardFrom(alice);
            Assert.That(() => game.PlayCardFrom(alice), Throws.InstanceOf<Exception>());

        }

        [Test]
        public void PlayerCanSnapEmptyStack()
        {
            var gameBuilder = new GameBuilder();
            Player alice = new Player("alice");
            gameBuilder.AddPlayer(alice);
            var game = gameBuilder.Build(new WinStateChecker(), Cards.With(new Card(Suit.Spades, Rank.Ace)));

            game.Snap(alice);
        }

        [Test]
        public void PlayerGainsSuccessfullySnappedCards()
        {
            var alwaysSnap = new Mock<IRule>();
            alwaysSnap.Setup(x => x.CanSnap(It.IsAny<Cards>())).Returns(true);
            var gameBuilder = new GameBuilder(alwaysSnap.Object);
            Player alice = new Player("alice");
            gameBuilder.AddPlayer(alice);
            var game = gameBuilder.Build(new WinStateChecker(), Cards.With(new Card(Suit.Spades, Rank.Queen)));
            game.PlayCardFrom(alice);
            game.Snap(alice);

            Assert.That(() => game.PlayCardFrom(alice), Throws.Nothing);
        }
    }

    public interface IWinStateChecker
    {
        /// <returns>winning player or null</returns>
        Player? GetWinningPlayer(Game game);
    }


    [TestFixture]
    public class GameBuilderTests
    {
       [Test]
        public void CanBuildGame()
        {
            var gameBuilder = new GameBuilder();
            var player = new Player("bob");
            gameBuilder.AddPlayer(player);
            var game = gameBuilder.Build(new WinStateChecker(), Cards.Empty());
            Assert.AreEqual(player, game.CurrentPlayer);
        }
    }

    public class WinStateChecker : IWinStateChecker
    {
        public Player? GetWinningPlayer(Game game)
        {
            return null;
        }
    }

    public struct Player
    {
        public readonly string Name;

        public Player(string name) : this()
        {
            Cards = Cards.Empty();
            Name = name;
        }

        public Cards Cards { get; private set; }
    }

    public class GameBuilder
    {
        private readonly IRule m_SnapValidator;
        private readonly List<Player> m_Players = new List<Player>();

        public GameBuilder(IRule snapValidator = null)
        {
            m_SnapValidator = snapValidator ?? new SnapValidator(Enumerable.Empty<IRule>());
        }

        public void AddPlayer(Player player)
        {
            m_Players.Add(player);
        }

        public Game Build(IWinStateChecker winStateChecker, IEnumerable<Card> deck)
        {
            if (deck.Any())
            {
                m_Players.First().Cards.AddToTop(deck.First());
            }

            return new Game(winStateChecker, m_SnapValidator, m_Players);
        }
    }

    public class Game
    {
        private readonly IWinStateChecker m_WinStateChecker;
        private readonly IRule m_SnapValidator;
        private readonly IReadOnlyCollection<Player> m_Players;
        private readonly Cards m_PlayedStack = Cards.Empty();

        public Game(IWinStateChecker winStateChecker, IRule snapValidator, IReadOnlyCollection<Player> players)
        {
            m_WinStateChecker = winStateChecker;
            m_SnapValidator = snapValidator;
            m_Players = players;
        }

        public Player CurrentPlayer
        {
            get { return m_Players.First(); }
        }

        public Player? Winner { get { return m_WinStateChecker.GetWinningPlayer(this); } }

        public void PlayCardFrom(Player player)
        {
            var cardPlayed = player.Cards.Pop();
            m_PlayedStack.AddToTop(cardPlayed);
        }

        public void Snap(Player player)
        {
            if (m_SnapValidator.CanSnap(m_PlayedStack))
            {
                player.Cards.AddToTop(m_PlayedStack.Pop());
            }

        }
    }
}
