using Hospital.Domain.Models;
using System.Linq.Expressions;

namespace Hospital.Application.Abstractions
{
    public interface IDoctorRepository
    {
        Task<Doctor> AddAsync(Doctor entity);
        Task UpdateAsync(Doctor entity);
        Task DeleteAsync(int id);
        Task<Doctor?> GetByIdAsync(int id);
        Task<List<Doctor>> SearchByPropertyAsync(Expression<Func<Doctor, bool>> entityPredicate);
        Task<List<Doctor>> GetAllAsync();
    }
}
