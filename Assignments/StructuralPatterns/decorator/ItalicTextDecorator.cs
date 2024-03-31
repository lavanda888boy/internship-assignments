namespace StructuralPatterns.decorator
{
    internal class ItalicTextDecorator : BaseTextDecorator
    {
        public ItalicTextDecorator(ITextComponent textComponent) : base(textComponent) { }

        public override string GetText()
        {
            return $"{_textComponent.GetText()}\nItalic styled";
        }
    }
}
