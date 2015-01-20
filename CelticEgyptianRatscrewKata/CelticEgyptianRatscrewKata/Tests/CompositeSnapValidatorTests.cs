using Moq;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    [TestFixture]
    public class CompositeSnapValidatorTests
    {
        [Test]
        public void EmptyStackIsNotSnap()
        {
            var stack = CreateStack();
            var isSnap = new CompositeSnapValidator().IsSnap(stack);
            Assert.That(isSnap, Is.False);
        }

        [Test]
        public void IsASnap()
        {
            var stack = CreateStack();

            var validatorMock1 = new Mock<ISnapValidator>();
            var validatorMock2 = new Mock<ISnapValidator>();

            validatorMock1.Setup(x => x.IsSnap(It.IsAny<Stack>())).Returns(true);
            validatorMock2.Setup(x => x.IsSnap(It.IsAny<Stack>())).Returns(false);

            var validator = new CompositeSnapValidator(validatorMock1.Object, validatorMock2.Object);

            var isSnap = validator.IsSnap(stack);
            Assert.That(isSnap, Is.True);
        }

        private static Stack CreateStack(params Card[] cards)
        {
            return new Stack(cards);
        }
    }
}
