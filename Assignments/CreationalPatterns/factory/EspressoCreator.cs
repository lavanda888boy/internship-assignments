using CreationalPatterns.builder;
using CreationalPatterns.entity;

namespace CreationalPatterns.factory
{
    internal class EspressoCreator : CoffeeCreator
    {
        public EspressoCreator(ICoffeeBuilder coffeeBuilder) : base(coffeeBuilder) { }

        public override Coffee CreateCoffee(CoffeeProperties cp)
        {
            return _coffeeBuilder.AddBlackCoffee()
                                 .GetCoffee();
        }
    }
}
