namespace StructuralPatterns.decorator
{
    internal abstract class BaseTextDecorator : ITextComponent
    {
        protected ITextComponent _textComponent;

        public BaseTextDecorator(ITextComponent textComponent)
        {
            _textComponent = textComponent;
        }

        public abstract string GetText();
    }
}
