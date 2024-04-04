using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IDepartmentRepository
    {
        Department Create(Department department);
        Department? Update(Department department);
        bool Delete(Department department);
        List<Department>? GetByProperty(Func<Department, bool> departmentPredicate);
        List<Department> GetAll();
        int GetLastId();
    }
}
