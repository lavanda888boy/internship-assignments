namespace OOP_practice.entities
{
    public class Car : Vehicle
    {
        private int numberOfDoors;
        public int NumberOfDoors { get { return numberOfDoors; } }

        public Car(string manufacturer, string model, int power, int numberOfDoors)
            : base(manufacturer, model, power)
        {
            this.numberOfDoors = numberOfDoors;
        }

        public override void Drive()
        {
            Console.WriteLine("The car drives");
        }

        public override void Drive(int speed)
        {
            Console.WriteLine("The car drives at speed " + speed);
        }
    }
}
