using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IDoctorRepository
    {
        Doctor Add(Doctor doctor);
        Doctor Update(Doctor doctor);
        bool Delete(Doctor doctor);
        List<Doctor>? GetByProperty(Func<Doctor, bool> doctorProperty);
        List<Doctor> GetAll();
    }
}
