using CreationalPatterns.entity;
using CreationalPatterns.utility;

namespace CreationalPatterns.factory
{
    internal interface ICoffeeCreator
    { 
        Coffee CreateCoffee(MilkType milkType);
    }
}
