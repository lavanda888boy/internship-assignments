using WorkingWithFilesAsync;

namespace WorkingWithFilesAsyncTest
{
    public class PatientRepositoryTest
    {
        private readonly IRepository<Patient> _patientRepository;

        public PatientRepositoryTest()
        {
            List<Patient> patients = new()
            {
                new Patient(1, "Walter", "White", "M", new List<string>() { "Steve" }, new List<string>() { "cancer" }),
                new Patient(2, "Mandy", "Smith", "W", new List<string>() { "Steve" }, new List<string>() { "cough" }),
            };
            _patientRepository =  new PatientRepository(patients);
        }

        [Fact]
        public void GetAllPatients()
        {
            Assert.Equal(
                expected: 2,
                actual: _patientRepository.GetAll().Count()
            );
        }

        [Theory]
        [MemberData(nameof(AddPatientData))]
        public void AddPatient_AddsToRepository(Patient patient)
        {
            _patientRepository.Add(patient);
            Assert.Contains(patient, _patientRepository.GetAll());
        }

        public static TheoryData<Patient> AddPatientData => new TheoryData<Patient>()
        {
            new Patient("Mike", "Ross", "M", new List<string>() { "Steve" }, new List<string>()),
            new Patient("Agatha", "Brenthon", "F", new List<string>() { "Harvey" }, new List<string>() { "cough", "migraine" })
        };


        [Theory]
        [MemberData(nameof(UpdatePatient_Data))]
        public void UpdatePatient_ExistingID(int patientID, Patient patient)
        {
            Assert.Throws<PatientDoesNotExistException>(() => _patientRepository.Update(patientID, patient));
        }

        public static TheoryData<int, Patient> UpdatePatient_Data => new TheoryData<int, Patient>()
        {
            { 10, new Patient("Mike", "Ross", "M", new List<string>() { "Steve" }, new List<string>()) },
            { 15, new Patient("Agatha", "Brenthon", "F", new List<string>() { "Harvey" }, new List<string>() { "cough", "migraine" }) }
        };


        [Theory]
        [MemberData(nameof(UpdateNonExistingPatient_Data))]
        public void UpdatePatient_NonExistingID_ThrowException(int patientID, Patient patient)
        {
            Assert.Throws<PatientDoesNotExistException>(() => _patientRepository.Update(patientID, patient));
        }

        public static TheoryData<int, Patient> UpdateNonExistingPatient_Data => new TheoryData<int, Patient>()
        {
            { 10, new Patient("Mike", "Ross", "M", new List<string>() { "Steve" }, new List<string>()) },
            { 15, new Patient("Agatha", "Brenthon", "F", new List<string>() { "Harvey" }, new List<string>() { "cough", "migraine" }) }
        };


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeletePatientByID_ExistingID(int patientID)
        {
            _patientRepository.DeleteById(patientID);
            Assert.DoesNotContain(_patientRepository.GetAll(), patient => patient.ID == patientID);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(-10)]
        public void DeletePatientByID_NonExistingID_ThrowException(int patientID)
        {
            Assert.Throws<PatientDoesNotExistException>(() => _patientRepository.DeleteById(patientID));
        }
    }
}
