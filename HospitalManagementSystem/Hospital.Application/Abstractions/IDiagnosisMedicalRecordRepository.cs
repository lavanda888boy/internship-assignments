using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IDiagnosisMedicalRecordRepository
    {
        DiagnosisMedicalRecord Create(DiagnosisMedicalRecord entity);
        bool Update(DiagnosisMedicalRecord entity);
        bool Delete(int id);
        DiagnosisMedicalRecord? GetById(int id);
        List<DiagnosisMedicalRecord>? SearchByProperty(Func<DiagnosisMedicalRecord, bool> entityPredicate);
        List<DiagnosisMedicalRecord> GetAll();
    }
}
