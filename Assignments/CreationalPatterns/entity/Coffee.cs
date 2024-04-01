using CreationalPatterns.exception;
using CreationalPatterns.utility;

namespace CreationalPatterns.entity
{
    internal abstract class Coffee
    {
        private readonly int sugarCountThreshold = 5;

        protected readonly string BaseCoffee = "Black coffee";
        protected MilkType milkType;
        protected int sugarCount;

        public Coffee()
        {
            milkType = MilkType.NO_TYPE;
            sugarCount = 0;
        }

        public void PrintIngredients()
        {
            Console.WriteLine(BaseCoffee);

            switch(milkType)
            {
                case MilkType.NO_TYPE:
                    break;
                case MilkType.REGULAR_MILK:
                    Console.WriteLine("Regular milk");
                    break;
                case MilkType.OAT_MILK:
                    Console.WriteLine("Oat milk");
                    break;
                case MilkType.SOY_MILK:
                    Console.WriteLine("Soy milk");
                    break;
            }

            Console.WriteLine($"Sugar count: {sugarCount}");
        }

        public void AddSugar(int sugarCount)
        {
            if (this.sugarCount + sugarCount > sugarCountThreshold)
            {
                throw new InvalidIngredientOperationException("Too much sugar in the coffee");
            }

            this.sugarCount += sugarCount;
        }

        public abstract void AddMilk(MilkType milkType);
    }
}
