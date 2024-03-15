using OOP_practice.entities;

namespace OOP_practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] vehicleDetails = { "engine", "wheel" };
            Vehicle v1 = new Vehicle("Scania", "R-90", 500, vehicleDetails);
            v1.Drive(speed: 80);

            Vehicle? v2 = v1.Clone() as Vehicle;
            if (v2 is not null)
            {
                Console.WriteLine(v2.Power);
            } else
            {
                Console.WriteLine("Cloned object is null");
            }

            foreach (var detail in v1)
            {
                Console.WriteLine(detail);
            }
        }
    }
}
