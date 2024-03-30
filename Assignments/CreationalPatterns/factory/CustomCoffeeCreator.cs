using CreationalPatterns.builder;
using CreationalPatterns.entity;

namespace CreationalPatterns.factory
{
    internal class CustomCoffeeCreator : CoffeeCreator
    {
        public CustomCoffeeCreator(ICoffeeBuilder coffeeBuilder, Coffee baseCoffee) : base(coffeeBuilder)
        {
            coffeeBuilder.Reset(baseCoffee);
        }

        public override Coffee CreateCoffee(CoffeeProperties cp)
        {
            return _coffeeBuilder.AddMilk(cp.MilkType)
                                 .AddSugar(cp.SugarCount)
                                 .GetCoffee();
        }
    }
}
