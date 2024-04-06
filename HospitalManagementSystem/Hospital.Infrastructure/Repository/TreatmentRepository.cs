using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.Repository
{
    public class TreatmentRepository : IRepository<Treatment>
    {
        private List<Treatment> _treatments = new();

        public Treatment Create(Treatment treatment)
        {
            _treatments.Add(treatment);
            return treatment;
        }

        public bool Delete(Treatment treatment)
        {
            return _treatments.Remove(treatment);
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
            var existingTreatment = _treatments.First(t => t.Id == treatment.Id);
            if (existingTreatment != null)
            {
                existingTreatment.PrescribedMedicine = treatment.PrescribedMedicine;
                existingTreatment.TreatmentDuration = treatment.TreatmentDuration;

                return true;
            }

            return false;
        }
    }
}
