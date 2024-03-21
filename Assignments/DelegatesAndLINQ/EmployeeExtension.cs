namespace DelegatesAndLINQ
{
    internal static class EmployeeExtension
    {
        public static void PrintEmployeeInfo(this Employee employee)
        {
            Console.WriteLine($"{employee.Surname} {employee.Name}\n{employee.Email}\n{employee.Phone}");
            Console.WriteLine($"Working day length (in hours): {employee.WorkingDayLength}");
            Console.WriteLine($"Employee is on vacation: {employee.IsOnVacation}\n");
        }
    }
}
