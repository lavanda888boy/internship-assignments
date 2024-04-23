using Hospital.Domain.Models;
using System.Linq.Expressions;

namespace Hospital.Application.Abstractions
{
    public interface IDepartmentRepository
    {
        Task<Department> AddAsync(Department entity);
        Task UpdateAsync(Department entity);
        Task DeleteAsync(int id);
        Task<Department?> GetByIdAsync(int id);
        Task<List<Department>> SearchByPropertyAsync(Expression<Func<Department, bool>> entityPredicate);
        Task<List<Department>> GetAllAsync();
    }
}
