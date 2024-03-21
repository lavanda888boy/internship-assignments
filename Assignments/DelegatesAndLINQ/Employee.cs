namespace DelegatesAndLINQ
{
    internal class Employee
    {
        private string _name;
        public string Name => _name;

        private string _surname;
        public string Surname => _surname;

        private string _email;
        public string Email
        {
            get => _email; set => _email = value;
        }

        private string _phone;
        public string Phone
        {
            get => _phone; set => _phone = value;
        }

        private int _workingDayLength;
        public int WorkingDayLength
        {
            get => _workingDayLength; set => _workingDayLength = value;
        }

        private bool _isOnVacation;
        public bool IsOnVacation
        {
            get => _isOnVacation; set => _isOnVacation = value;
        }

        public Employee(string name, string surname, string email, string phone, int workingDayLength, bool isOnVacation)
        {
            _name = name;
            _surname = surname;
            Email = email;
            Phone = phone;
            WorkingDayLength = workingDayLength;
            IsOnVacation = isOnVacation;
        }
    }
}
