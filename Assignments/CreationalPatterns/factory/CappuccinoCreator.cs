using CreationalPatterns.entity;
using CreationalPatterns.utility;

namespace CreationalPatterns.factory
{
    internal class CappuccinoCreator : ICoffeeCreator
    {
        public Coffee CreateCoffee(MilkType milkType)
        {
            return new Cappuccino(milkType);
        }
    }
}
