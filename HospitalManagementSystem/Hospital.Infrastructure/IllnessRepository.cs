using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure
{
    internal class IllnessRepository : IIllnessRepository
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

        public List<Illness> GetByProperty(Func<Illness, bool> illnessProperty)
        {
            return _illnesses.Where(illnessProperty).ToList();
        }

        public int GetLastId()
        {
            return _illnesses.Any() ? _illnesses.Max(illness => illness.Id) : 0;
        }

        public Illness? Update(Illness illness)
        {
            var existingIllness = _illnesses.FirstOrDefault(i => i.Id == illness.Id);
            if (existingIllness != null)
            {
                existingIllness.Name = illness.Name;
                existingIllness.DateOfDiagnosis = illness.DateOfDiagnosis;
                existingIllness.DateOfTreatmentEnd = illness.DateOfTreatmentEnd;
                existingIllness.DiagnosisDoctor = illness.DiagnosisDoctor;
            }
            return existingIllness;
        }
    }
}
