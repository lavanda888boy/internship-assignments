using Hospital.Domain.Models;
using System.Linq.Expressions;

namespace Hospital.Application.Abstractions
{
    public interface IRegularMedicalRecordRepository
    {
        Task<RegularMedicalRecord> AddAsync(RegularMedicalRecord entity);
        Task UpdateAsync(RegularMedicalRecord entity);
        Task DeleteAsync(int id);
        Task<RegularMedicalRecord?> GetByIdAsync(int id);
        Task<List<RegularMedicalRecord>> SearchByPropertyAsync
            (Expression<Func<RegularMedicalRecord, bool>> entityPredicate);
        Task<List<RegularMedicalRecord>> GetAllAsync();
    }
}
