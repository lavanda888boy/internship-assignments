namespace StructuralPatterns.decorator
{
    internal class TextComponent : ITextComponent
    {
        private string _text;
   
        public TextComponent(string text)
        {
            _text = text;
        }

        public string GetText()
        {
            return _text;
        }
    }
}
