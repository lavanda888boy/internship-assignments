using StructuralPatterns.decorator;

namespace StructuralPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITextComponent text = new TextComponent("Some text");
            Console.WriteLine(text.GetText());
        }
    }
}
