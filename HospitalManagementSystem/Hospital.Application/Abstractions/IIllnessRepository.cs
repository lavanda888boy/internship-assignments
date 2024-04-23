using Hospital.Domain.Models;
using System.Linq.Expressions;

namespace Hospital.Application.Abstractions
{
    public interface IIllnessRepository
    {
        Task<Illness> AddAsync(Illness entity);
        Task UpdateAsync(Illness entity);
        Task DeleteAsync(int id);
        Task<Illness?> GetByIdAsync(int id);
        Task<List<Illness>> SearchByPropertyAsync(Expression<Func<Illness, bool>> entityPredicate);
        Task<List<Illness>> GetAllAsync();
    }
}
