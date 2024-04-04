using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IPatientRepository
    {
        Patient Add(Patient patient);
        Patient Update(Patient patient);
        bool Delete(Patient patient);
        List<Patient>? GetByProperty(Func<Patient, bool> patientProperty);
        List<Patient> GetAll();
    }
}
