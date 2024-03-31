namespace StructuralPatterns.decorator
{
    internal class ColorTextDecorator : BaseTextDecorator
    {
        public string Color { get; set; }

        public ColorTextDecorator(ITextComponent textComponent, string color) : base(textComponent)
        {
            Color = color;
        }

        public override string GetText()
        {
            return $"{_textComponent.GetText()}\n<<Color: {Color}>>";
        }
    }
}
