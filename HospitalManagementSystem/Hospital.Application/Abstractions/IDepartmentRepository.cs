using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IDepartmentRepository
    {
        Department Create(Department entity);
        bool Update(Department entity);
        bool Delete(int id);
        Department GetById(int id);
        List<Department>? SearchByProperty(Func<Department, bool> entityPredicate);
        List<Department> GetAll();
    }
}
