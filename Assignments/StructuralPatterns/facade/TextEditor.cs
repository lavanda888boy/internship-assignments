using StructuralPatterns.decorator;

namespace StructuralPatterns.facade
{
    internal class TextEditor : ITextEditor
    {
        public ITextComponent Text { get; set; }
        private Stack<(ITextComponent, char)> _history;
        private HashSet<char> _usedCommands;

        public TextEditor(ITextComponent textComponent)
        {
            Text = textComponent;
            _history = new Stack<(ITextComponent, char)>();
            Reset();
        }

        public ITextComponent MakeBold()
        {
            if (!_usedCommands.Contains('b'))
            {
                _history.Push((Text, 'b'));
                Text = new BoldTextDecorator(Text);
                _usedCommands.Add('b');
            }

            return Text;
        }

        public ITextComponent MakeColored(string color)
        {
            if (!_usedCommands.Contains('c'))
            {
                _history.Push((Text, 'c'));
                Text = new ColorTextDecorator(Text, color);
                _usedCommands.Add('c');
            }

            return Text;
        }

        public ITextComponent MakeItalic()
        {
            if (!_usedCommands.Contains('i'))
            {
                _history.Push((Text, 'i'));
                Text = new ItalicTextDecorator(Text);
                _usedCommands.Add('i');
            }

            return Text;
        }

        public ITextComponent Underline()
        {
            if (!_usedCommands.Contains('u'))
            {
                _history.Push((Text, 'u'));
                Text = new UnderlineTextDecorator(Text);
                _usedCommands.Add('u');
            }
            return Text;
        }

        public ITextComponent Undo()
        {
            if (_history.Count > 0)
            {
                (Text, char command) = _history.Pop();
                _usedCommands.Remove(command);
                Console.WriteLine("Reversed last edit operation\n");
            }

            return Text;
        }

        public void Reset()
        {
            _usedCommands = new HashSet<char>();
        }
    }
}
