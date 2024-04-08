using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IPatientRepository
    {
        Patient Create(Patient entity);
        bool Update(Patient entity);
        bool Delete(int id);
        Patient? GetById(int id);
        List<Patient>? SearchByProperty(Func<Patient, bool> entityPredicate);
        List<Patient> GetAll();
    }
}
