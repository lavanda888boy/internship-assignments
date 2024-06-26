﻿using CreationalPatterns.exception;
using CreationalPatterns.utility;

namespace CreationalPatterns.entity
{
    internal class FlatWhite : Coffee
    {
        public FlatWhite(MilkType milkType) : base()
        {
            this.milkType = milkType;
        }

        public override void AddMilk(MilkType milkType)
        {
            if (milkType != MilkType.NO_TYPE)
            {
                throw new InvalidIngredientOperationException("Cannot add milk to Flatwhite");
            }
        }
    }
}
