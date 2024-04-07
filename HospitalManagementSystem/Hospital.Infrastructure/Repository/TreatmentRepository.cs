using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.Repository
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private List<Treatment> _treatments = new();

        public Treatment Create(Treatment treatment)
        {
            _treatments.Add(treatment);
            return treatment;
        }

        public bool Delete(int treatmentId)
        {
            var treatmentToRemove = GetById(treatmentId);
            if (treatmentToRemove is null)
            {
                return false;
            }

            _treatments.Remove(treatmentToRemove);
            return true;
        }

        public List<Treatment> GetAll()
        {
            return _treatments;
        }

        public Treatment GetById(int id)
        {
            return _treatments.First(t => t.Id == id);
        }

        public List<Treatment>? SearchByProperty(Func<Treatment, bool> treatmentPredicate)
        {
            return _treatments.Where(treatmentPredicate).ToList();
        }

        public bool Update(Treatment treatment)
        {
            var existingTreatment = GetById(treatment.Id);
            if (existingTreatment != null)
            {
                int index = _treatments.IndexOf(existingTreatment);
                _treatments[index] = treatment;
                return true;
            }
            return false;
        }
    }
}
