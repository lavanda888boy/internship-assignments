namespace StructuralPatterns.utility
{
    internal class TextCommand
    {
        public TextCommandName CommandName { get; set; }
        public string? Color { get; set; }

        public TextCommand(TextCommandName commandName)
        {
            CommandName = commandName;
        }
    }

    internal enum TextCommandName
    {
        BOLD,
        ITALIC,
        UNDERLINED,
        COLOR
    }
}
