namespace DelegatesAndLINQ
{
    internal class Employee : IComparable<Employee>
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

        public int CompareTo(Employee? e)
        {
            if (e is null) throw new ArgumentNullException("Employee for comparison is null");

            if (_name == e.Name && _surname == e.Surname && _email == e.Email && _phone == e.Phone && _workingDayLength == e.WorkingDayLength && _isOnVacation == e.IsOnVacation)
            {
                return 0;
            }

            if (_workingDayLength < e.WorkingDayLength)
            {
                return -1;
            } else 
            {
                return 1;
            }
        }
    }
}
