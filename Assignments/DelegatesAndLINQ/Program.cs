namespace DelegatesAndLINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee john = new Employee("Smith", "John", "john@mail.com", "762589", 8, false);
            Employee mike = new Employee("Ross", "Mike", "mike@mail.com", "214153", 12, false);
            Employee agatha = new Employee("Brenton", "Agatha", "agatha@mail.com", "235869", 6, true);
            Employee harvey = new Employee("Specter", "Harvey", "harvey@mail.com", "849254", 12, false);
            Employee jim = new Employee("Halpert", "Jim", "jim@mail.com", "389575", 4, true);
            Employee dwight = new Employee("Schrute", "Dwight", "dwight@mail.com", "959559", 10, false);

            EmployeeCollection ec = [ john, mike, agatha, harvey, jim, dwight ];

            List<Laptop> devices =
            [
                new Laptop("Dell", "Latitude", john),
                new Laptop("Dell", "XPS", mike),
                new Laptop("Dell", "Vostro", agatha),
                new Laptop("Lenovo", "Thinkpad", harvey),
                new Laptop("Lenovo", "Thinkbook", jim),
                new Laptop("Macbook", "Pro", dwight)
            ];

            /*var e = ec.Find(employee => !employee.IsOnVacation);*/

            Func<Employee, bool> SearchDelegate = delegate (Employee employee)
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
            var e = ec.Find(SearchDelegate);

            /*Func<Employee, bool> SearchDelegate = (employee) => employee.Phone.StartsWith("2");
            var e = ec.Find(SearchDelegate);*/

            /*var e = ec.Employees.Where(employee => !employee.IsOnVacation)
                                .OrderByDescending(employee => employee.WorkingDayLength);*/

            /*var e = ec.Employees.Where(employee => employee.WorkingDayLength < 10)
                                .OrderBy(employee => employee.Surname)
                                .Skip(1)
                                .Select(e => new
                                {
                                    Surname = e.Surname,
                                    Name = e.Name,
                                    WorkingDayLength = e.WorkingDayLength
                                });*/

            foreach (var em in e)
            {
                //Console.WriteLine(em);
                em.PrintEmployeeInfo();
            }
        }
    }
}
