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
            TextCommand command = new TextCommand(TextCommandName.BOLD);
            AddCommandToHistoryIfNotPresent(command);
            return GetFormattedTextRepresentation();
        }

        public override ITextComponent MakeColored(string color)
        {
            TextCommand command = new TextCommand(TextCommandName.COLOR);
            command.Color = color;
            AddCommandToHistoryIfNotPresent(command);
            return GetFormattedTextRepresentation();
        }

        public override ITextComponent MakeItalic()
        {
            TextCommand command = new TextCommand(TextCommandName.ITALIC);
            AddCommandToHistoryIfNotPresent(command);
            return GetFormattedTextRepresentation();
        }

        public override ITextComponent Underline()
        {
            TextCommand command = new TextCommand(TextCommandName.UNDERLINED);
            AddCommandToHistoryIfNotPresent(command);
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

        private void AddCommandToHistoryIfNotPresent(TextCommand command)
        {
            if (!(command.CommandName == TextCommandName.COLOR && _usedCommands.Any(c => c.CommandName == TextCommandName.COLOR)) && 
                !_usedCommands.Any(c => c.CommandName == command.CommandName && c.Color == command.Color))
            {
                _usedCommands.Add(command);
            }

        }
    }
}
