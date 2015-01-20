using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    [TestFixture]
    public class StandardSnapValidatorTests
    {
        [Test]
        public void EmptyStackIsNotSnap()
        {
            var stack = new Stack(Enumerable.Empty<Card>());
            var isSnap = IsSnap(stack);
            Assert.That(isSnap, Is.False);
        }

        private bool IsSnap(Stack stack)
        {
            throw new NotImplementedException();
        }
    }
}
