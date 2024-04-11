using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IRegularMedicalRecordRepository
    {
        RegularMedicalRecord Create(RegularMedicalRecord entity);
        bool Update(RegularMedicalRecord entity);
        bool Delete(int id);
        RegularMedicalRecord? GetById(int id);
        List<RegularMedicalRecord> SearchByProperty(Func<RegularMedicalRecord, bool> entityPredicate);
        List<RegularMedicalRecord> GetAll();
    }
}
