using CreationalPatterns.exception;
using CreationalPatterns.utility;

namespace CreationalPatterns.entity
{
    internal class Espresso : Coffee
    {
        public Espresso() : base() { }

        public override void AddMilk(MilkType milkType)
        {
            if (milkType != MilkType.NO_TYPE)
            {
                throw new InvalidIngredientOperationException("Espresso already has milk");
            }

            this.milkType = milkType;
        }
    }
}
