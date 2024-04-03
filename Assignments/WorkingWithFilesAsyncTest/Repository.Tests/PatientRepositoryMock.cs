using WorkingWithFilesAsync;

namespace WorkingWithFilesAsyncTest.Repository.Tests
{
    public class PatientRepositoryMock
    {
        public List<Patient> Patients { get; set; }

        public PatientRepositoryMock(List<Patient> patients)
        {
            Patients = patients;
        }

        public void Add(Patient patient)
        {
            Patients.Add(patient);
        }

        public void DeleteById(int id)
        {
            var p = Patients.Find(p => p.ID == id);
            if (p is null)
            {
                throw new PatientDoesNotExistException($"Patient with id = {id} cannot be deleted, it does not exist");
            }
            else
            {
                Patients.Remove(p);
            }
        }

        public IEnumerable<Patient> GetAll()
        {
            return Patients;
        }

        public Patient GetById(int id)
        {
            var p = Patients.Find(p => p.ID == id);
            if (p is null)
            {
                throw new PatientDoesNotExistException($"Patient with id = {id} cannot be extracted, it does not exist");
            }
            else
            {
                return p;
            }
        }

        public void Update(int id, Patient patient)
        {
            var p = Patients.Find(p => p.ID == id);
            if (p is null)
            {
                throw new PatientDoesNotExistException("Patient cannot be updated, it does not exist", patient);
            }
            else
            {
                int index = Patients.IndexOf(p);
                Patients[index] = patient;
            }
        }
    }
}
