using CreationalPatterns.builder;
using CreationalPatterns.entity;

namespace CreationalPatterns.factory
{
    internal class FlatWhiteCreator : CoffeeCreator
    {
        public FlatWhiteCreator(ICoffeeBuilder coffeeBuilder) : base(coffeeBuilder) { }

        public override Coffee CreateCoffee(CoffeeProperties cp)
        {
            return _coffeeBuilder.AddBlackCoffee()
                                 .AddBlackCoffee()
                                 .AddMilk(cp.MilkType)
                                 .GetCoffee();
        }
    }
}
