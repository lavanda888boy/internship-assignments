using Hospital.Domain.Models;
using System.Linq.Expressions;

namespace Hospital.Application.Abstractions
{
    public interface IPatientRepository
    {
        Task<Patient> AddAsync(Patient entity);
        Task UpdateAsync(Patient entity);
        Task DeleteAsync(int id);
        Task<Patient?> GetByIdAsync(int id);
        Task<List<Patient>> SearchByPropertyAsync(Expression<Func<Patient, bool>> entityPredicate);
        Task<List<Patient>> GetAllAsync();
    }
}
