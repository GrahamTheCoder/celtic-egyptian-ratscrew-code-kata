using System.Linq;

namespace CelticEgyptianRatscrewKata
{
    internal class CompositeSnapValidator : ISnapValidator
    {
        private readonly ISnapValidator[] m_Validators;

        public CompositeSnapValidator(params ISnapValidator[] validators)
        {
            m_Validators = validators;
        }

        public bool IsSnap(Stack stack)
        {
            return m_Validators.Any(v => v.IsSnap(stack));
        }
    }
}