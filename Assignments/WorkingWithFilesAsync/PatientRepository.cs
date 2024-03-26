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
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patients;
        }

        public Patient GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Patient entity)
        {
            throw new NotImplementedException();
        }
    }
}
