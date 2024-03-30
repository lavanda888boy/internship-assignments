using CreationalPatterns.builder;
using CreationalPatterns.entity;

namespace CreationalPatterns.factory
{
    internal abstract class CoffeeCreator
    {
        protected ICoffeeBuilder _coffeeBuilder;

        public CoffeeCreator(ICoffeeBuilder coffeeBuilder)
        {
            _coffeeBuilder = coffeeBuilder;
        }

        public abstract Coffee CreateCoffee(CoffeeProperties cp);
    }
}
