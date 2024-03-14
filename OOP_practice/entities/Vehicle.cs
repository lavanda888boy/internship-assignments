namespace OOP_practice.entities
{
    public class Vehicle : ICloneable
    {
        private string _manufacturer;
        public string Manufacturer { get { return _manufacturer; } }

        private string _model;
        public string Model { get { return _model; } }

        private int _power;
        public int Power
        {
            get
            {
                return _power;
            }

            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Power cannot be negative or zero");
                } else
                {
                    _power = value;
                }
            }
        }

        public Vehicle(string manufacturer, string model, int power)
        {
            _manufacturer = manufacturer;
            _model = model;
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

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
