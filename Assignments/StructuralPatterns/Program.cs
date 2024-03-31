using StructuralPatterns.decorator;
using StructuralPatterns.facade;

namespace StructuralPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITextComponent text = new TextComponent("Some text");
            ITextEditor textEditor = new TextEditor(text);
            Console.WriteLine(textEditor.MakeColored("red").GetText());
            Console.WriteLine(textEditor.MakeBold().GetText());
            Console.WriteLine(textEditor.Undo().GetText());
        }
    }
}
