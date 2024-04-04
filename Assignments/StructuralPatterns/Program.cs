using StructuralPatterns.decorator;
using StructuralPatterns.facade;

namespace StructuralPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BaseTextEditor editor = new HistoryTextEditor();

            while(true)
            {
                Console.WriteLine("Welcome to the text editor! Please enter the text to edit:");
                var text = Console.ReadLine();
                Console.WriteLine();

                if (text != ""  &&  text is not null)
                {
                    ProcessCommands(text, editor);
                }
            }
        }

        static void ProcessCommands(string text, BaseTextEditor textEditor)
        {
            textEditor.BaseText = new TextComponent(text);

            while (true)
            {
                Console.WriteLine("Enter the command to edit the text:\n");
                Console.WriteLine("b - make it bold");
                Console.WriteLine("i - make it italic");
                Console.WriteLine("u - make it underlined");
                Console.WriteLine("c - apply color to it");
                Console.WriteLine("l - list all previous commands");
                Console.WriteLine("z - undo operation");
                Console.WriteLine("s - enter new text\n");

                var option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "b":
                        Console.WriteLine(textEditor.MakeBold().GetText());
                        break;
                    case "i":
                        Console.WriteLine(textEditor.MakeItalic().GetText());
                        break;
                    case "u":
                        Console.WriteLine(textEditor.Underline().GetText());
                        break;
                    case "c":
                        Console.WriteLine(textEditor.MakeColored(ProcessColorAddition()).GetText());
                        break;
                    case "l":
                        ((HistoryTextEditor)textEditor).PrintAllUsedCommands();
                        break;
                    case "z":
                        Console.WriteLine(((HistoryTextEditor)textEditor).Undo(GetCommandToUndo()).GetText());
                        break;
                    case "s":
                        return;
                    default:
                        break;
                }

                Console.WriteLine();
            }
        }

        static string ProcessColorAddition()
        {
            Console.WriteLine("Enter the color to apply:");
            var color = Console.ReadLine();
            Console.WriteLine();

            return color;
        }

        static int GetCommandToUndo()
        {
            Console.WriteLine("Enter the number of the command to undo:");
            var commandNumber = Console.ReadLine();
            Console.WriteLine();
            return int.Parse(commandNumber);
        }
    }
}
