using CreationalPatterns.entity;

namespace CreationalPatterns.builder
{
    internal interface ICoffeeBuilder
    {
        void Reset();
        void Reset(Coffee coffee);
        ICoffeeBuilder AddBlackCoffee();
        ICoffeeBuilder AddMilk(string milkType);
        ICoffeeBuilder AddSugar(int sugarCount);
        Coffee GetCoffee();
    }
}
