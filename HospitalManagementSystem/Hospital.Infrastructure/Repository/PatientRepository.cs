using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private List<Patient> _patients = new();

        public Patient Create(Patient patient)
        {
            _patients.Add(patient);
            return patient;
        }

        public bool Delete(int patientId)
        {
            var patientToRemove = GetById(patientId);
            if (patientToRemove is null)
            {
                return false;
            }

            _patients.Remove(patientToRemove);
            return true;
        }

        public List<Patient> GetAll()
        {
            return _patients;
        }

        public Patient GetById(int id)
        {
            return _patients.First(p => p.Id == id);
        }

        public List<Patient>? SearchByProperty(Func<Patient, bool> patientProperty)
        {
            return _patients.Where(patientProperty).ToList();
        }

        public bool Update(Patient patient)
        {
            var existingPatient = GetById(patient.Id);
            if (existingPatient != null)
            {
                int index = _patients.IndexOf(existingPatient);
                _patients[index] = patient;
                return true;
            }
            return false;
        }
    }
}
