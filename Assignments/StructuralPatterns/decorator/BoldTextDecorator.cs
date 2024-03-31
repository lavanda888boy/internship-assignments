namespace StructuralPatterns.decorator
{
    internal class BoldTextDecorator : BaseTextDecorator
    {
        public BoldTextDecorator(ITextComponent textComponent) : base(textComponent) { }

        public override string GetText()
        {
            return $"{_textComponent.GetText()}\n<<Bold>>"; 
        }
    }
}
