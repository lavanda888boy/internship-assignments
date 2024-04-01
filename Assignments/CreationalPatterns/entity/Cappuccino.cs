using CreationalPatterns.exception;
using CreationalPatterns.utility;

namespace CreationalPatterns.entity
{
    internal class Cappuccino : Coffee
    {
        public Cappuccino(MilkType milkType) : base()
        {
            this.milkType = milkType;
        }

        public override void AddMilk(MilkType milkType)
        {
            if (milkType != MilkType.NO_TYPE)
            {
                throw new InvalidIngredientOperationException("Cannot add milk to Cappuccino");
            }
        }
    }
}
