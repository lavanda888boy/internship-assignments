namespace DelegatesAndLINQ
{
    internal class Laptop
    {
        private string _manufacturer;
        public string Manufacturer => _manufacturer;

        private string _model;
        public string Model => _model;

        private Employee _owner;
        public Employee Owner => _owner;

        public Laptop(string manufacturer, string model, Employee owner)
        {
            _manufacturer = manufacturer;
            _model = model;
            _owner = owner;
        }
    }
}
