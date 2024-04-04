using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IIllnessRepository
    {
        Illness Add(Illness illness);
        Illness Update(Illness illness);
        bool Delete(Illness illness);
        List<Illness>? GetByProperty(Func<Illness, bool> illnessProperty);
        List<Illness> GetAll();
    }
}
