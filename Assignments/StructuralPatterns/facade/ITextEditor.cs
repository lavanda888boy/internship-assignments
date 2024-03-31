using StructuralPatterns.decorator;

namespace StructuralPatterns.facade
{
    internal interface ITextEditor
    {
        ITextComponent MakeBold();
        ITextComponent MakeItalic();
        ITextComponent Underline();
        ITextComponent MakeColored(string color);
        ITextComponent Undo();
    }
}
