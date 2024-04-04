using StructuralPatterns.decorator;

namespace StructuralPatterns.facade
{
    internal abstract class BaseTextEditor
    {
        public ITextComponent BaseText { get; set; }

        public BaseTextEditor()
        {
            BaseText = new TextComponent("");
        }

        public abstract ITextComponent MakeBold();
        public abstract ITextComponent MakeColored(string color);
        public abstract ITextComponent MakeItalic();
        public abstract ITextComponent Underline();
    }
}
