using StructuralPatterns.decorator;
using StructuralPatterns.facade;

namespace StructuralPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextEditor editor = new TextEditor(new TextComponent(""));

            while(true)
            {
                Console.WriteLine("Welcome to the text editor! Please enter the text to edit:");
                var text = Console.ReadLine();
                Console.WriteLine();

                if (text != "")
                {
                    ProcessCommands(text, editor);
                }
            }
        }

        static void ProcessCommands(string text, ITextEditor textEditor)
        {
            ((TextEditor) textEditor).Text = new TextComponent(text);

            while (true)
            {
                Console.WriteLine("Enter the command to edit the text:\n");
                Console.WriteLine("b - make it bold");
                Console.WriteLine("i - make it italic");
                Console.WriteLine("u - make it underlined");
                Console.WriteLine("c - apply color to it");
                Console.WriteLine("z - undo last operation");
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
                    case "z":
                        Console.WriteLine(textEditor.Undo().GetText());
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
    }
}
