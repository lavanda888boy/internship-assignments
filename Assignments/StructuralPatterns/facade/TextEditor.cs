using StructuralPatterns.decorator;

namespace StructuralPatterns.facade
{
    internal class TextEditor : ITextEditor
    {
        public ITextComponent Text { get; set; }
        private Stack<ITextComponent> _history;

        public TextEditor(ITextComponent textComponent)
        {
            Text = textComponent;
            _history = new Stack<ITextComponent>();
        }

        public ITextComponent MakeBold()
        {
            _history.Push(Text);
            Text = new BoldTextDecorator(Text);
            return Text;
        }

        public ITextComponent MakeColored(string color)
        {
            _history.Push(Text);
            Text = new ColorTextDecorator(Text, color);
            return Text;
        }

        public ITextComponent MakeItalic()
        {
            _history.Push(Text);
            Text = new ItalicTextDecorator(Text);
            return Text;
        }

        public ITextComponent Underline()
        {
            _history.Push(Text);
            Text = new UnderlineTextDecorator(Text);
            return Text;
        }

        public ITextComponent Undo()
        {
            if (_history.Count > 0)
            {
                Text = _history.Pop();
                Console.WriteLine("Reversed last edit operation\n");
            }

            return Text;
        }
    }
}
