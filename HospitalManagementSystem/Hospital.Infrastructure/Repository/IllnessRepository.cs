using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.Repository
{
    public class IllnessRepository : IRepository<Illness>
    {
        private List<Illness> _illnesses = new();
        public Illness Create(Illness illness)
        {
            _illnesses.Add(illness);
            return illness;
        }

        public bool Delete(Illness illness)
        {
            return _illnesses.Remove(illness);
        }

        public List<Illness> GetAll()
        {
            return _illnesses;
        }

        public Illness GetById(int id)
        {
            return _illnesses.First(i => i.Id == id);
        }

        public List<Illness>? SearchByProperty(Func<Illness, bool> illnessPredicate)
        {
            return _illnesses.Where(illnessPredicate).ToList();
        }

        public bool Update(Illness illness)
        {
            var existingIllness = _illnesses.FirstOrDefault(i => i.Id == illness.Id);
            if (existingIllness != null)
            {
                existingIllness.Name = existingIllness.Name;
                existingIllness.IllnessSeverity = existingIllness.IllnessSeverity;
                return true;
            }
            return false;
        }
    }
}
