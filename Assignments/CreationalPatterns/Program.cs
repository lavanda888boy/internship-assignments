using CreationalPatterns.entity;
using CreationalPatterns.exception;
using CreationalPatterns.factory;
using CreationalPatterns.utility;

namespace CreationalPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICoffeeCreator c = new CappuccinoCreator();
            Coffee coffee = c.CreateCoffee(MilkType.SOY_MILK);

            try
            {
                ICoffeeCreator cc = new CustomCoffeeCreator(coffee, 6);
                cc.CreateCoffee(MilkType.NO_TYPE).PrintIngredients();
            }
            catch (InvalidIngredientOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
