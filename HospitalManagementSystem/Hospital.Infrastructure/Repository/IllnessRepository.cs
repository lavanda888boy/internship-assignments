using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.Repository
{
    public class IllnessRepository : IIllnessRepository
    {
        private List<Illness> _illnesses = new();
        public Illness Create(Illness illness)
        {
            _illnesses.Add(illness);
            return illness;
        }

        public bool Delete(int illnessId)
        {
            var illnessToRemove = GetById(illnessId);
            if (illnessToRemove is null)
            {
                return false;
            }

            _illnesses.Remove(illnessToRemove);
            return true;
        }

        public List<Illness> GetAll()
        {
            return _illnesses;
        }

        public Illness? GetById(int id)
        {
            return _illnesses.FirstOrDefault(i => i.Id == id);
        }

        public List<Illness> SearchByProperty(Func<Illness, bool> illnessPredicate)
        {
            return _illnesses.Where(illnessPredicate).ToList();
        }

        public bool Update(Illness illness)
        {
            var existingIllness = GetById(illness.Id);
            if (existingIllness != null)
            {
                int index = _illnesses.IndexOf(existingIllness);
                _illnesses[index] = illness;
                return true;
            }
            return false;
        }
    }
}
