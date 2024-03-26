namespace WorkingWithFilesAsync
{
    internal class PatientRepository : IRepository<Patient>
    {
        private List<Patient> _patients;

        public PatientRepository()
        {
            _patients = new List<Patient>();
        }

        public void Add(Patient patient)
        {
            _patients.Add(patient);
        }

        public void DeleteById(int id)
        {
            var p = _patients.Find(p => p.ID == id); 
            if (p is null)
            {
                throw new PatientDoesNotExistException($"Patient with id = {id} cannot be deleted, it does not exist");
            } 
            else
            {
                _patients.Remove(p);
            }
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patients;
        }

        public Patient GetById(int id)
        {
            var p = _patients.Find(p => p.ID == id);
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
            var p = _patients.Find(p => p.ID == id);
            if (p is null)
            {
                throw new PatientDoesNotExistException("Patient cannot be updated, it does not exist", patient);
            } 
            else
            {
                int index = _patients.IndexOf(p);
                _patients[index] = patient;
            }
        }
    }
}
