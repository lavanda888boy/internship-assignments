using Hospital.Domain.Models;
using System.Linq.Expressions;

namespace Hospital.Application.Abstractions
{
    public interface ITreatmentRepository
    {
        Task<Treatment> AddAsync(Treatment entity);
        Task UpdateAsync(Treatment entity);
        Task DeleteAsync(int id);
        Task<Treatment?> GetByIdAsync(int id);
        Task<List<Treatment>> SearchByPropertyAsync
            (Expression<Func<Treatment, bool>> entityPredicate);
        Task<List<Treatment>> GetAllAsync();
    }
}
