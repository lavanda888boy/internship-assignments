using Hospital.Domain.Models;
using System.Linq.Expressions;

namespace Hospital.Application.Abstractions
{
    public interface IDiagnosisMedicalRecordRepository
    {
        Task<DiagnosisMedicalRecord> AddAsync(DiagnosisMedicalRecord entity);
        Task UpdateAsync(DiagnosisMedicalRecord entity);
        Task DeleteAsync(int id);
        Task<DiagnosisMedicalRecord?> GetByIdAsync(int id);
        Task<List<DiagnosisMedicalRecord>> SearchByPropertyAsync
            (Expression<Func<DiagnosisMedicalRecord, bool>> entityPredicate);
        Task<List<DiagnosisMedicalRecord>> GetAllAsync();
    }
}
