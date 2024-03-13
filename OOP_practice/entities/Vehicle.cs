namespace OOP_practice.entities
{
    public class Vehicle
    {
        private string manufacturer;
        public string Manufacturer { get { return manufacturer; } }

        private string model;
        public string Model { get { return model; } }

        private int power;
        public int Power
        {
            get
            {
                return power;
            }

            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Power cannot be negative or zero");
                } else
                {
                    power = value;
                }
            }
        }

        public Vehicle(string manufacturer, string model, int power)
        {
            this.manufacturer = manufacturer;
            this.model = model;
            Power = power;
        }

        public virtual void Drive()
        {
            Console.WriteLine("The vehicle drives");
        }

        public virtual void Drive(int speed)
        {
            Console.WriteLine("The vehicle drives at speed " + speed);
        }
    }
}
