namespace DelegatesAndLINQ
{
    internal static class EmployeeCollectionExtension
    {
        public static void CollectiveAction(this EmployeeCollection ec, Action<Employee> action)
        {
            foreach (var employee in ec.Employees)
            {
                action(employee);
            }
        }
    }
}
