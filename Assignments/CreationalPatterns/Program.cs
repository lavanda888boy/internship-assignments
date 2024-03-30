using CreationalPatterns.builder;
using CreationalPatterns.entity;
using CreationalPatterns.factory;

namespace CreationalPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICoffeeBuilder coffeeBuilder = new CoffeeBuilder();
            CoffeeCreator espressoCreator = new EspressoCreator(coffeeBuilder);
            CoffeeCreator cappuccinoCreator = new CappuccinoCreator(coffeeBuilder);
            CoffeeCreator flatWhiteCreator = new FlatWhiteCreator(coffeeBuilder);

            
        }
    }
}
