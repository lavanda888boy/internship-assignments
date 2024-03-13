using OOP_practice.entities;

namespace OOP_practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vehicle v = new Vehicle("Volvo", "XC-90", 300);
            v.Drive();
            Console.WriteLine(v.Model);
        }
    }
}
