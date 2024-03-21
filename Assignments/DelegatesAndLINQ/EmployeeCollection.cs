using System.Collections;

namespace DelegatesAndLINQ
{
    internal class EmployeeCollection : IEnumerable
    {
        private readonly int _initCollectionSize = 16;
        private List<Employee> _employees;
        public List<Employee> Employees { get => _employees; }

        public EmployeeCollection()
        {
            _employees = new List<Employee>(_initCollectionSize);
        }

        public IEnumerator GetEnumerator() => _employees.GetEnumerator();

        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }

        public List<Employee> Find(Func<Employee, bool> compare)
        {
            List<Employee> filteredEmployees = new List<Employee>();

            foreach (var employee in _employees)
            {
                if (compare(employee))
                {
                    filteredEmployees.Add(employee);
                }
            }
            return filteredEmployees;
        }
    }
}
