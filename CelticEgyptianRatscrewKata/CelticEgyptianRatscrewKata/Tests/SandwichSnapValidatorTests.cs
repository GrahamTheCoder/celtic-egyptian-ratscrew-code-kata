using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    [TestFixture]
    public class SandwichSnapValidatorTests
    {
        private readonly ISnapValidator m_SnapValidator = new SandwichSnapValidator();

        [Test]
        public void EmptyStackIsNotSnap()
        {
            var stack = CreateStack();
            var isSnap = m_SnapValidator.IsSnap(stack);
            Assert.That(isSnap, Is.False);
        }

        private static Stack CreateStack(params Card[] cards)
        {
            return new Stack(cards);
        }
    }
}
