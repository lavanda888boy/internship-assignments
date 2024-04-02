using CreationalPatterns.entity;
using CreationalPatterns.utility;

namespace CreationalPatterns.factory
{
    internal class CustomCoffeeCreator : ICoffeeCreator
    {
        private Coffee baseCoffee;
        public int SugarCount { get; set; }

        public CustomCoffeeCreator(Coffee baseCoffee, int sugarCount)
        {
            this.baseCoffee = baseCoffee;
            SugarCount = sugarCount;
        }

        public Coffee CreateCoffee(MilkType milkType)
        {
            baseCoffee.AddMilk(milkType);
            baseCoffee.AddSugar(SugarCount);
            return baseCoffee;
        }
    }
}
