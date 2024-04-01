using CreationalPatterns.entity;
using CreationalPatterns.utility;

namespace CreationalPatterns.factory
{
    internal class FlatWhiteCreator : ICoffeeCreator
    {
        public Coffee CreateCoffee(MilkType milkType)
        {
            return new FlatWhite(milkType);
        }
    }
}
