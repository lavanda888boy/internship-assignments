using System.Collections;

namespace DelegatesAndLINQ
{
    internal class EmployeeCollection : IEnumerable
    {
        private readonly int _initCollectionSize = 16;
        private List<Employee> _employees;

        public EmployeeCollection()
        {
            _employees = new List<Employee>(_initCollectionSize);
        }

        public IEnumerator GetEnumerator() => _employees.GetEnumerator();
    }
}
