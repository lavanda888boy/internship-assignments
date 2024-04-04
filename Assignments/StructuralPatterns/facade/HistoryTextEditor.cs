using StructuralPatterns.decorator;
using StructuralPatterns.utility;

namespace StructuralPatterns.facade
{
    internal class HistoryTextEditor : BaseTextEditor
    {
        private List<TextCommand> _usedCommands = new List<TextCommand>();

        public HistoryTextEditor() : base() { }

        public override ITextComponent MakeBold()
        {
            ToggleBold();
            return GetFormattedTextRepresentation();
        }

        public override ITextComponent MakeColored(string color)
        {
            ToggleColor(color);
            return GetFormattedTextRepresentation();
        }

        public override ITextComponent MakeItalic()
        {
            ToggleItalic();
            return GetFormattedTextRepresentation();
        }

        public override ITextComponent Underline()
        {
            ToggleUnderline();
            return GetFormattedTextRepresentation();
        }

        public ITextComponent Undo(int textCommandIndex)
        {
            _usedCommands.RemoveAt(textCommandIndex - 1);
            return GetFormattedTextRepresentation();
        }

        public void PrintAllUsedCommands()
        {
            _usedCommands.ForEach(c => Console.WriteLine($"{_usedCommands.IndexOf(c) + 1}. {c.CommandName} {c.Color ?? ""}"));
            Console.WriteLine();
        }

        private ITextComponent GetFormattedTextRepresentation()
        {
            ITextComponent formattedText = BaseText;

            foreach (var command in _usedCommands)
            {
                switch (command.CommandName)
                {
                    case TextCommandName.BOLD:
                        formattedText = new BoldTextDecorator(formattedText);
                        break;
                    case TextCommandName.ITALIC:
                        formattedText = new ItalicTextDecorator(formattedText);
                        break;
                    case TextCommandName.UNDERLINED:
                        formattedText = new UnderlineTextDecorator(formattedText);
                        break;
                    case TextCommandName.COLOR:
                        formattedText = new ColorTextDecorator(formattedText, command.Color);
                        break;
                    default:
                        break;
                }
            }

            return formattedText;
        }

        private void ToggleBold()
        {
            var isBold = _usedCommands.FirstOrDefault(c => c.CommandName == TextCommandName.BOLD);
            if (isBold != null)
            {
                _usedCommands.Remove(isBold);
            }
            else
            {
                _usedCommands.Add(new TextCommand(TextCommandName.BOLD));
            }
        }

        private void ToggleItalic()
        {
            var isItalic = _usedCommands.FirstOrDefault(c => c.CommandName == TextCommandName.ITALIC);
            if (isItalic != null)
            {
                _usedCommands.Remove(isItalic);
            }
            else
            {
                _usedCommands.Add(new TextCommand(TextCommandName.ITALIC));
            }
        }

        private void ToggleUnderline()
        {
            var isUnderlined = _usedCommands.FirstOrDefault(c => c.CommandName == TextCommandName.UNDERLINED);
            if (isUnderlined != null)
            {
                _usedCommands.Remove(isUnderlined);
            }
            else
            {
                _usedCommands.Add(new TextCommand(TextCommandName.UNDERLINED));
            }
        }
        
        private void ToggleColor(string color)
        {
            var isColored = _usedCommands.FirstOrDefault(c => c.CommandName == TextCommandName.COLOR);
            if (isColored != null)
            {
                _usedCommands.Remove(isColored);
            }

            TextCommand c = new TextCommand(TextCommandName.COLOR);
            c.Color = color;
            _usedCommands.Add(c);
        }
    }
}
