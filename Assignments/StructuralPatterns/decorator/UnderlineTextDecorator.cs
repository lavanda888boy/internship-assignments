namespace StructuralPatterns.decorator
{
    internal class UnderlineTextDecorator : BaseTextDecorator
    {
        public UnderlineTextDecorator(ITextComponent textComponent) : base(textComponent) { }

        public override string GetText()
        {
            return $"{_textComponent.GetText()}\nUnderlined";
        }
    }
}
