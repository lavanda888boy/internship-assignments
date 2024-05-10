using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.UnitTests.MedicalRecords.Test.CommandHandlers.Test
{
    public class AddNewRegularMedicalRecordTest
    {
        private Mock<IRepository<RegularMedicalRecord>> _recordRepositoryMock;
        private Mock<IRepository<Patient>> _patientRepositoryMock;
        private Mock<IRepository<Doctor>> _doctorRepositoryMock;

        public AddNewRegularMedicalRecordTest()
        {
            _recordRepositoryMock = new();
            _patientRepositoryMock = new();
            _doctorRepositoryMock = new();
        }

        [Fact]
        public async Task AddNewRegularMedicalRecord_ShouldReturnRegularMedicalRecordId()
        {
            var patient = new Patient()
            {
                Id = 1,
                Name = "Steve",
                Surname = "Smith",
                Age = 25,
                Gender = Gender.M,
                Address = "Some address",
                PhoneNumber = "068749856",
                InsuranceNumber = "AB45687952",
                DoctorsPatients = new List<DoctorsPatients>()
                {
                    new DoctorsPatients()
                    {
                        PatientId = 1,
                        DoctorId = 1
                    }
                }
            };

            var doctor = new Doctor()
            {
                Id = 1,
                Name = "Mick",
                Surname = "Mouse",
                Address = "Chisinau",
                PhoneNumber = "079854623",
                Department = new Department()
                {
                    Id = 1,
                    Name = "Heart diseases",
                },
                WorkingHours = new DoctorSchedule()
                {
                    Id = 1,
                    StartShift = new TimeSpan(7, 0, 0),
                    EndShift = new TimeSpan(16, 0, 0),
                },
                DoctorsPatients = new List<DoctorsPatients>()
                {
                    new DoctorsPatients()
                    {
                        PatientId = 1,
                        DoctorId = 1
                    }
                }
            };

            string notes = "Some notes";

            var record = new RegularMedicalRecord()
            {
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes
            };

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patient.Id)).Returns(Task.FromResult(patient));
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctor.Id)).Returns(Task.FromResult(doctor));
            _recordRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<RegularMedicalRecord>())).Returns(Task.FromResult(record));

            var command = new AddNewRegularMedicalRecord(patient.Id, doctor.Id, notes);
            var handler = new AddNewRegularMedicalRecordHandler(_patientRepositoryMock.Object, 
                _doctorRepositoryMock.Object, _recordRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(record.Id, result);
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
            _recordRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<RegularMedicalRecord>()), Times.Once);
        }

        [Fact]
        public async Task AddNewRegularMedicalRecord_ShouldThrowException_PatientDoesNotExist()
        {
            int patientId = 2;
            int doctorId = 3;
            string notes = "Some notes";
            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patientId));

            var command = new AddNewRegularMedicalRecord(patientId, doctorId, notes);
            var handler = new AddNewRegularMedicalRecordHandler(_patientRepositoryMock.Object, 
                _doctorRepositoryMock.Object, _recordRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patientId), Times.Once);
        }

        [Fact]
        public async Task AddNewRegularMedicalRecord_ShouldThrowException_DoctorDoesNotExist()
        {
            var patient = new Patient()
            {
                Id = 1,
                Name = "Steve",
                Surname = "Smith",
                Age = 25,
                Gender = Gender.M,
                Address = "Some address",
                PhoneNumber = "068749856",
                InsuranceNumber = "AB45687952"
            };

            int doctorId = 3;
            string notes = "Some notes";

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patient.Id)).Returns(Task.FromResult(patient));
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctorId));

            var command = new AddNewRegularMedicalRecord(patient.Id, doctorId, notes);
            var handler = new AddNewRegularMedicalRecordHandler(_patientRepositoryMock.Object, 
                _doctorRepositoryMock.Object, _recordRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctorId), Times.Once);
        }

        [Fact]
        public async Task AddNewRegularMedicalRecord_ShouldThrowException_PatientIsNotAssignedToTheDoctor()
        {
            var patient = new Patient()
            {
                Id = 1,
                Name = "Steve",
                Surname = "Smith",
                Age = 25,
                Gender = Gender.M,
                Address = "Some address",
                PhoneNumber = "068749856",
                InsuranceNumber = "AB45687952"
            };

            var doctor = new Doctor()
            {
                Id = 1,
                Name = "Mick",
                Surname = "Mouse",
                Address = "Chisinau",
                PhoneNumber = "079854623",
                Department = new Department()
                {
                    Id = 1,
                    Name = "Heart diseases",
                },
                WorkingHours = new DoctorSchedule()
                {
                    Id = 1,
                    StartShift = new TimeSpan(7, 0, 0),
                    EndShift = new TimeSpan(16, 0, 0),
                }
            };

            string notes = "Some notes";

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patient.Id)).Returns(Task.FromResult(patient));
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctor.Id)).Returns(Task.FromResult(doctor));

            var command = new AddNewRegularMedicalRecord(patient.Id, doctor.Id, notes);
            var handler = new AddNewRegularMedicalRecordHandler(_patientRepositoryMock.Object, 
                _doctorRepositoryMock.Object, _recordRepositoryMock.Object);

            await Assert.ThrowsAsync<PatientDoctorMisassignationException>(() => handler.Handle(command, default));

            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctor.Id), Times.Once);
        }
    }
}
