namespace OOP_practice
{
    public class Motorcycle : Vehicle
    {
        private bool _hasFairing;
        public bool HasFairing { get { return _hasFairing; } set { _hasFairing = value; } }

        public Motorcycle(string manufacturer, string model, int power, string[] details, bool hasFairing)
            : base(manufacturer, model, power, details)
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
