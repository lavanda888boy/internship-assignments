using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IIllnessRepository
    {
        Illness Create(Illness entity);
        bool Update(Illness entity);
        bool Delete(int id);
        Illness GetById(int id);
        List<Illness>? SearchByProperty(Func<Illness, bool> entityPredicate);
        List<Illness> GetAll();
    }
}
