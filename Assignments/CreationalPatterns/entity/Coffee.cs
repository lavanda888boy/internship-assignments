namespace CreationalPatterns.entity
{
    internal class Coffee
    {
        public List<string> Ingredients { get; set; }

        public void PrintIngredients()
        {
            Console.WriteLine(string.Join(", ", Ingredients));
        }
    }
}
