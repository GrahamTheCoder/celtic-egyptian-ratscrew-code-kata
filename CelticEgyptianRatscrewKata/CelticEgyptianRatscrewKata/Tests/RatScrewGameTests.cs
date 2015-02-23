using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CelticEgyptianRatscrewKata.GameSetup;
using Moq;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    [TestFixture]
    class RatScrewGameTests
    {
        [Test]
        public void CheckItDoesntExplode()
        {
            //var mockConsoleInterface = new Mock<I>
            var setupMock = new Mock<IGameSetupUserInterface>();
            var gamePlayMock = new Mock<IGamePlayUserInterface>();
            
            var game = new RatScrewGame(setupMock.Object, gamePlayMock.Object);
            game.Play();

            setupMock.Verify(x => x.GetPlayerInfoFromUserLazily(), Times.Once());
            char input;
            gamePlayMock.Verify(x => x.TryReadUserInput(out input), Times.Once());
            
        }
    }
}
