using CreationalPatterns.builder;
using CreationalPatterns.entity;

namespace CreationalPatterns.factory
{
    internal class CappuccinoCreator : CoffeeCreator
    {
        public CappuccinoCreator(ICoffeeBuilder coffeeBuilder) : base(coffeeBuilder) { }

        public override Coffee CreateCoffee(CoffeeProperties cp)
        {
            return _coffeeBuilder.AddBlackCoffee()
                                 .AddMilk(cp.MilkType)
                                 .GetCoffee();
        }
    }
}
