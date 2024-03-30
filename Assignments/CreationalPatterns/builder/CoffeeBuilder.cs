using CreationalPatterns.entity;

namespace CreationalPatterns.builder
{
    internal class CoffeeBuilder : ICoffeeBuilder
    {
        private Coffee _coffee;

        public CoffeeBuilder()
        {
            Reset();
        }

        public ICoffeeBuilder AddBlackCoffee()
        {
            _coffee.Ingredients.Add("Black Coffee");
            return this;
        }

        public ICoffeeBuilder AddMilk(string milkType)
        {
            if (milkType != "") _coffee.Ingredients.Add(milkType);
            return this;
        }

        public ICoffeeBuilder AddSugar(int sugarCount)
        {
            _coffee.Ingredients.AddRange(Enumerable.Repeat("Sugar", sugarCount));
            return this;
        }

        public void Reset()
        {
            _coffee = new Coffee();
            _coffee.Ingredients = new List<string>();
        }

        public void Reset(Coffee coffee)
        {
            _coffee = coffee;
        }

        public Coffee GetCoffee()
        {
            Coffee c = _coffee;
            Reset();
            return c;
        }
    }
}
