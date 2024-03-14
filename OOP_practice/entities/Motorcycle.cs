namespace OOP_practice.entities
{
    public class Motorcycle : Vehicle
    {
        private bool _hasFairing;
        public bool HasFairing { get { return _hasFairing; } set { _hasFairing = value; } }

        public Motorcycle(string manufacturer, string model, int power, bool hasFairing)
            : base(manufacturer, model, power)
        {
            _hasFairing = hasFairing;
        }

        public override void Drive()
        {
            Console.WriteLine("The motorcycle drives");
        }

        public override void Drive(int speed)
        {
            Console.WriteLine("The motorcycle drives at speed " + speed);
        }
    }
}
