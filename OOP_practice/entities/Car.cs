namespace OOP_practice.entities
{
    public class Car : Vehicle
    {
        private int _numberOfDoors;
        public int NumberOfDoors { get { return _numberOfDoors; } }

        public Car(string manufacturer, string model, int power, string[] details, int numberOfDoors)
            : base(manufacturer, model, power, details)
        {
            _numberOfDoors = numberOfDoors;
        }

        public override void Drive()
        {
            Console.WriteLine("The car drives");
        }

        public override void Drive(int speed)
        {
            Console.WriteLine("The car drives at speed " + speed);
        }

        public void Drive(int speed, string drivingMode = "2WD")
        {
            Console.WriteLine("The car drives at speed " + speed);
            Console.WriteLine("The driving mode is " + drivingMode);
        }
    }
}
