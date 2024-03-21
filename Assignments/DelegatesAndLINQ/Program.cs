using System;

namespace DelegatesAndLINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeCollection ec = new EmployeeCollection();
            ec.Add(new Employee("Smith", "John", "john@mail.com", "762589", 8, false));
            ec.Add(new Employee("Brenton", "Agatha", "agatha@mail.com", "235869", 6, true));
            ec.Add(new Employee("Ross", "Mike", "mike@mail.com", "214153", 12, false));

            /*var e = ec.Find(employee => !employee.IsOnVacation);*/

            /*Func<Employee, bool> SearchDelegate = delegate (Employee employee)
            {
                if (employee.WorkingDayLength < 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            var e = ec.Find(SearchDelegate);*/

            Func<Employee, bool> SearchDelegate = (employee) => employee.Phone.StartsWith("2");
            var e = ec.Find(SearchDelegate);

            foreach (var employee in e)
            {
                employee.PrintEmployeeInfo();
            }
        }
    }
}
