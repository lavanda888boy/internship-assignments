using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface ITreatmentRepository
    {
        Treatment Create(Treatment entity);
        bool Update(Treatment entity);
        bool Delete(int id);
        Treatment GetById(int id);
        List<Treatment>? SearchByProperty(Func<Treatment, bool> entityPredicate);
        List<Treatment> GetAll();
    }
}
