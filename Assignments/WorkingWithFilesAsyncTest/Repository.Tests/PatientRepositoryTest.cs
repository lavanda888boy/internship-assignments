using WorkingWithFilesAsync;

namespace WorkingWithFilesAsyncTest.Repository.Tests
{
    public class PatientRepositoryTest
    {
        protected readonly PatientRepositoryMock _patientRepository;

        public PatientRepositoryTest()
        {
            List<Patient> patients = new()
            {
                new Patient(1, "Walter", "White", "M", new List<string>() { "Steve" }, new List<string>() { "cancer" }),
                new Patient(2, "Mandy", "Smith", "W", new List<string>() { "Steve" }, new List<string>() { "cough" }),
            };

            _patientRepository = new PatientRepositoryMock(patients);
        }
    }

    public class GetAllPatientsTests : PatientRepositoryTest
    {
        [Fact]
        public void GetAllPatients()
        {
            Assert.Equal(
                expected: 2,
                actual: _patientRepository.GetAll().Count()
            );
        }
    }

    public class GetPatientByIDTests : PatientRepositoryTest
    {
        [Theory]
        [MemberData(nameof(GetPatientByID_ExistingPatient_Data))]
        public void GetPatientByID_ExistingID(int patientID, Patient existingPatient)
        {
            Assert.Equal(
                expected: existingPatient,
                actual: _patientRepository.GetById(patientID)
            );
        }

        public static TheoryData<int, Patient> GetPatientByID_ExistingPatient_Data => new TheoryData<int, Patient>()
        {
            { 1, new Patient(1, "Walter", "White", "M", new List<string>() { "Steve" }, new List<string>() { "cancer" })},
            { 2, new Patient(2, "Mandy", "Smith", "W", new List<string>() { "Steve" }, new List<string>() { "cough" })},
        };


        [Theory]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(-10)]
        public void GetPatientByID_NonExistingID_ThrowException(int patientID)
        {
            Assert.Throws<PatientDoesNotExistException>(() => _patientRepository.GetById(patientID));
        }
    }

    public class AddPatientTests : PatientRepositoryTest
    {
        [Theory]
        [MemberData(nameof(AddPatientData))]
        public void AddPatient_AddsToRepository(Patient patient)
        {
            _patientRepository.Add(patient);
            Assert.Contains(patient, _patientRepository.Patients);
        }

        public static TheoryData<Patient> AddPatientData => new TheoryData<Patient>()
        {
            new Patient("Mike", "Ross", "M", new List<string>() { "Steve" }, new List<string>()),
            new Patient("Agatha", "Brenthon", "F", new List<string>() { "Harvey" }, new List<string>() { "cough", "migraine" })
        };
    }

    public class UpdatePatientTests : PatientRepositoryTest
    {
        [Fact]
        public void UpdatePatient_ExistingID_UpdateName()
        {
            Patient patient = new Patient(1, "Mike", "White", "M", new List<string>() { "Steve" }, new List<string>() { "cancer" });

            _patientRepository.Update(patient.ID, patient);
            var updatedPatient = _patientRepository.Patients.First(p => p.ID == patient.ID);
            Assert.Equal(
                expected: "Mike",
                actual: updatedPatient.Name
            );
        }

        [Fact]
        public void UpdatePatient_ExistingID_UpdateSurname()
        {
            Patient patient = new Patient(1, "Walter", "Smith", "M", new List<string>() { "Steve" }, new List<string>() { "cancer" });

            _patientRepository.Update(patient.ID, patient);
            var updatedPatient = _patientRepository.Patients.First(p => p.ID == patient.ID);
            Assert.Equal(
                expected: "Smith",
                actual: updatedPatient.Surname
            );
        }

        [Fact]
        public void UpdatePatient_ExistingID_UpdateAssignedDoctors()
        {
            Patient patient = new Patient(2, "Mandy", "Smith", "W", new List<string>() { "Steve", "Harvey" }, new List<string>() { "cough" });

            _patientRepository.Update(patient.ID, patient);
            var updatedPatient = _patientRepository.Patients.First(p => p.ID == patient.ID);
            Assert.Equal(
                expected: new List<string> { "Steve", "Harvey" },
                actual: updatedPatient.AssignedDoctors
            );
        }

        [Fact]
        public void UpdatePatient_ExistingID_UpdateIllnesses()
        {
            Patient patient = new Patient(2, "Mandy", "Smith", "W", new List<string>() { "Steve" }, new List<string>() { "migraine" });

            _patientRepository.Update(patient.ID, patient);
            var updatedPatient = _patientRepository.Patients.First(p => p.ID == patient.ID);
            Assert.Equal(
                expected: new List<string> { "migraine" },
                actual: updatedPatient.Illnesses
            );
        }


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
    }

    public class DeletePatientByIDTests : PatientRepositoryTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeletePatientByID_ExistingID(int patientID)
        {
            _patientRepository.DeleteById(patientID);
            Assert.DoesNotContain(_patientRepository.Patients, patient => patient.ID == patientID);
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
