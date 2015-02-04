using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void NoOneHasWonAtStartOfTwoPlayerGame()
        {
            var gameBuilder = new GameBuilder();
            gameBuilder.AddPlayer(new Player("alice"));
            gameBuilder.AddPlayer(new Player("bob"));
            var winner = gameBuilder.Build().Winner;
            Assert.That(winner, Is.Null);
        }

        [Test]
        public void PlayerOneWinsInOnePlayerGame()
        {
            var gameBuilder = new GameBuilder();
            var alice = new Player("alice");
            gameBuilder.AddPlayer(alice);
            var winner = gameBuilder.Build().Winner;
            Assert.That(winner, Is.EqualTo(alice));
        }
    }


    [TestFixture]
    public class GameBuilderTests
    {
        [Test]
        public void CanInitialize()
        {
            Assert.DoesNotThrow(() => new GameBuilder());
        }

        [Test]
        public void CanAddPlayer()
        {
            var gameBuilder = new GameBuilder();
            Assert.DoesNotThrow(() =>
            {
                gameBuilder.AddPlayer(new Player("bob"));
            });
        }

        [Test]
        public void CanBuildGame()
        {
            var gameBuilder = new GameBuilder();
            var player = new Player("bob");
            gameBuilder.AddPlayer(player);
            var game = gameBuilder.Build();
            Assert.AreEqual(player, game.CurrentPlayer);
        }
    }

    public class Player
    {
        public readonly string Bob;

        public Player(string bob)
        {
            Bob = bob;
        }
    }

    public class GameBuilder
    {
        private List<Player> m_Players = new List<Player>();

        public void AddPlayer(Player player)
        {
            m_Players.Add(player);
        }

        public Game Build()
        {
            return new Game(m_Players);
        }
    }

    public class Game
    {
        private readonly IReadOnlyCollection<Player> m_Players;

        public Game(IReadOnlyCollection<Player> players)
        {
            m_Players = players;
        }

        public Player CurrentPlayer
        {
            get { return m_Players.First(); }
        }

        public Player Winner { get; private set; }
    }
}
