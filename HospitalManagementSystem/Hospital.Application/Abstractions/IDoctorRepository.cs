using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IDoctorRepository
    {
        Doctor Create(Doctor entity);
        bool Update(Doctor entity);
        bool Delete(int id);
        Doctor GetById(int id);
        List<Doctor>? SearchByProperty(Func<Doctor, bool> entityPredicate);
        List<Doctor> GetAll();
    }
}
