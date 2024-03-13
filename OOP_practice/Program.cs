using OOP_practice.entities;

namespace OOP_practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car c = new Car("Volvo", "XC-90", 300, 5);
            c.Power = -20;
        }
    }
}
