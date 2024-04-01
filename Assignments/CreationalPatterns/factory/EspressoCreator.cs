using CreationalPatterns.entity;
using CreationalPatterns.utility;

namespace CreationalPatterns.factory
{
    internal class EspressoCreator : ICoffeeCreator
    {
        public Coffee CreateCoffee(MilkType milkType)
        {
            return new Espresso();
        }
    }
}
