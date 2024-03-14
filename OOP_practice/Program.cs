using OOP_practice.entities;

namespace OOP_practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vehicle v1 = new Vehicle("Scania", "R-90", 500);
            Vehicle v2 = (Vehicle) v1.Clone();

            v2.Power = 550;
            Console.WriteLine(v1.Power);
        }
    }
}
