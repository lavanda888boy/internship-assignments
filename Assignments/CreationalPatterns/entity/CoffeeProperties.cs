namespace CreationalPatterns.entity
{
    internal class CoffeeProperties
    {
        public string MilkType { get; set; }
        public int SugarCount { get; set; }

        public CoffeeProperties(string milkType, int sugarCount)
        {
            MilkType = milkType;
            SugarCount = sugarCount;
        }
    }
}
