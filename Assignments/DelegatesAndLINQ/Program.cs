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
                new Laptop("Macbook", "Pro", dwight),
                new Laptop("Macbook", "Air", agatha),
                new Laptop("Acer", "Aspire", mike),
            ];

            /*Action<Employee> action = (employee) => employee.WorkingDayLength += 1;
            ec.CollectiveAction(action);

            foreach (Employee employee in ec)
            {
                employee.PrintEmployeeInfo();
            }*/

            AdvancedLINQ(ec.Employees, devices);

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
                                });
*/
            /*foreach (var em in e)
            {
                //Console.WriteLine(em);
                em.PrintEmployeeInfo();
            }*/
        }

        static void AdvancedLINQ(List<Employee> emps, List<Laptop> dvs)
        {
            /*var employeeDevice = emps.Join(dvs,
                                     emp => emp,
                                     dvs => dvs.Owner,
                                     (emp, dvs) => new { Owner = emp.Surname, DeviceName = $"{dvs.Manufacturer} {dvs.Model}" });

            Console.WriteLine("Join method:\n");
            foreach (var item in employeeDevice)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var groupedEmployeeDevice = emps.GroupJoin(dvs,
                                 emp => emp,
                                 dvs => dvs.Owner,
                                 (emp, dvs) => new { Owner = emp.Surname, Devices = dvs });

            Console.WriteLine("GroupJoin method:\n");
            foreach (var item in groupedEmployeeDevice)
            {
                Console.Write($"{item.Owner} => ");
                foreach (var newItem in item.Devices)
                {
                    Console.Write($"{newItem.Model} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            var groupedLaptops = dvs.GroupBy(d => d.Manufacturer)
                                    .Select(el => new { Manufacturer = el.Key, Count = el.Count() });

            Console.WriteLine("GroupBy method:\n");
            foreach (var item in groupedLaptops)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var averageWorkingTime = emps.Average(emp => emp.WorkingDayLength);
            Console.WriteLine($"Average working time: {double.Round(averageWorkingTime, 2)}\n");

            var laptopUsersCount = dvs.Select(d => new { d.Manufacturer })
                                      .Count(el => el.Manufacturer == "Dell");
            Console.WriteLine($"There are {laptopUsersCount} people with Dell laptops\n");

            var workingEmployeesPresent = emps.Any(e => !e.IsOnVacation);
            Console.WriteLine($"{workingEmployeesPresent}\n");

            try
            {
                Console.WriteLine("Single method:");
                var singleManufacturerLaptop = dvs.Single(l => l.Manufacturer == "Lenovo");
                Console.WriteLine(singleManufacturerLaptop.Owner.Name);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }  */


            Console.WriteLine("\nSet operation:");
            var empty = Enumerable.Empty<Employee>().ToList();
            empty.Add(new Employee("Smith", "John", "john@mail.com", "762589", 8, false));
            empty.Add(new Employee("Bale", "Morgan", "morgan@mail.com", "258149", 8, false));
            empty.Add(new Employee("Schrute", "Dwight", "dwight@mail.com", "959559", 10, false));

            var empsNames = emps.Select(e => new { FullName = $"{e.Surname} {e.Name}" });
            var newEmpsNames = empty.Select(e => new { FullName = $"{e.Surname} {e.Name}" });

            var union = newEmpsNames.Union(empsNames).ToDictionary(item => item.FullName, item => item);
            var concat = emps.Concat(empty);
            var intersect = empsNames.Intersect(newEmpsNames).ToHashSet();
            var except = newEmpsNames.Except(empsNames).ToArray();
            foreach (var item in union)
            {
                Console.WriteLine($"{item.Key} {item.Value}");
            }
            Console.WriteLine();
            foreach (var item in concat)
            {
                item.PrintEmployeeInfo();
            }
            Console.WriteLine();
            foreach (var item in intersect)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nScalar operations:\n");
            var lazyEmpDayLength = emps.Min(e => e.WorkingDayLength);
            Console.WriteLine(lazyEmpDayLength);

            var hardworkingEmpDayLength = emps.Max(e => e.WorkingDayLength);
            Console.WriteLine(hardworkingEmpDayLength);

            var totalWorkingHours = emps.Sum(e => e.WorkingDayLength);
            Console.WriteLine(totalWorkingHours);

            var names = new List<string>{ "John", "Paul", "Peter", "Mike", "Tom" };
            var otherNames = new List<string> { "Paul", "Peter", "Tom", "Mike", "John" };

            var combinations = names.Zip(otherNames, (n, o) => (n + " " + o));
            foreach (var item in combinations)
            {
                Console.WriteLine(item);
            }

            var stringOfNames = names.Aggregate((current, next) => current + ", " + next);
            Console.WriteLine(stringOfNames);

            if (names.Contains("Paul"))
            {
                Console.WriteLine("Paul is on the list");
            }

            if (names.SequenceEqual(otherNames))
            {
                Console.WriteLine("Sequences are equal");
            }

            Console.WriteLine();
            var firstEmp = emps.FirstOrDefault(e => e.WorkingDayLength < 4, new Employee("John", "Doe", "mail.com", "111111", 0, true));
            firstEmp.PrintEmployeeInfo();

            Console.WriteLine();
            var dev = dvs.ElementAt(2);
            Console.WriteLine(dev.Model);

            List<Employee> es = new List<Employee>();
            var newEs = es.DefaultIfEmpty(new Employee("John", "Doe", "mail.com", "111111", 0, true));
            newEs.ElementAt(0).PrintEmployeeInfo();
        }   
    }
}
