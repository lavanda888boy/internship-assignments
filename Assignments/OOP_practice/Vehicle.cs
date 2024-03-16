using System.Collections;

namespace OOP_practice
{
    public class Vehicle : IEnumerable, ICloneable
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

        private string[] _details;

        public Vehicle(string manufacturer, string model, int power, string[] details)
        {
            _manufacturer = manufacturer;
            _model = model;
            Power = power;
            _details = details;
        }

        public IEnumerator GetEnumerator() => new VehicleEnumerator(_details);

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

        private class VehicleEnumerator : IEnumerator
        {
            private string[] _details;
            private int _position = -1;

            public VehicleEnumerator(string[] details) => _details = details;

            public object Current
            {
                get
                {
                    if (_position == -1 || _position >= _details.Length)
                        throw new ArgumentException();
                    return _details[_position];
                }
            }

            public bool MoveNext()
            {
                if (_position < _details.Length - 1)
                {
                    _position++;
                    return true;
                }
                else
                    return false;
            }

            public void Reset() => _position = -1;
        }
    }
}
