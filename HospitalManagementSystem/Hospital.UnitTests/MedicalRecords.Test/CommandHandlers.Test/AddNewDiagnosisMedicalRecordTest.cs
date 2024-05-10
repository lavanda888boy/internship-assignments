using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.UnitTests.MedicalRecords.Test.CommandHandlers.Test
{
    public class AddNewDiagnosisMedicalRecordTest
    {
        private Mock<IRepository<DiagnosisMedicalRecord>> _recordRepositoryMock;
        private Mock<IRepository<Patient>> _patientRepositoryMock;
        private Mock<IRepository<Doctor>> _doctorRepositoryMock;
        private Mock<IRepository<Illness>> _illnessRepositoryMock;

        public AddNewDiagnosisMedicalRecordTest()
        {
            _recordRepositoryMock = new();
            _patientRepositoryMock = new();
            _doctorRepositoryMock = new();
            _illnessRepositoryMock = new();
        }

        [Fact]
        public async Task AddNewDiagnosisMedicalRecord_ShouldReturnDiagnosisMedicalRecordId()
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

            var record = new DiagnosisMedicalRecord()
            {
                Id = 1,
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes,
                DiagnosedIllness = new Illness()
                {
                    Id = 1,
                    Name = "Flu"
                },
                ProposedTreatment = new Treatment()
                {
                    PrescribedMedicine = "Panadol",
                    DurationInDays = 5
                }
            };

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patient.Id)).Returns(Task.FromResult(patient));
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctor.Id)).Returns(Task.FromResult(doctor));
            _illnessRepositoryMock.Setup(repo => repo.GetByIdAsync(record.DiagnosedIllness.Id)).Returns(Task.FromResult(record.DiagnosedIllness));
            _recordRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<DiagnosisMedicalRecord>())).Returns(Task.FromResult(record));

            var command = new AddNewDiagnosisMedicalRecord(patient.Id, doctor.Id, notes, record.DiagnosedIllness.Id,
                record.ProposedTreatment.PrescribedMedicine, record.ProposedTreatment.DurationInDays);
            var handler = new AddNewDiagnosisMedicalRecordHandler(_patientRepositoryMock.Object,
                _doctorRepositoryMock.Object, _illnessRepositoryMock.Object, _recordRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(record.Id, result);
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctor.Id), Times.Once);
            _illnessRepositoryMock.Verify(repo => repo.GetByIdAsync(record.DiagnosedIllness.Id), Times.Once);
            _recordRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<DiagnosisMedicalRecord>()), Times.Once);
        }

        [Fact]
        public async Task AddNewRegularMedicalRecord_ShouldThrowException_PatientDoesNotExist()
        {
            int patientId = 2;
            int doctorId = 3;
            string notes = "Some notes";
            int illnessId = 1;
            string prescribedMedicine = "Ibuprofen";
            int duration = 5;
            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patientId));

            var command = new AddNewDiagnosisMedicalRecord(patientId, doctorId, notes, illnessId,
                prescribedMedicine, duration);
            var handler = new AddNewDiagnosisMedicalRecordHandler(_patientRepositoryMock.Object,
                _doctorRepositoryMock.Object, _illnessRepositoryMock.Object, _recordRepositoryMock.Object);

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
            int illnessId = 1;
            string prescribedMedicine = "Ibuprofen";
            int duration = 5;

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patient.Id)).Returns(Task.FromResult(patient));
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctorId));

            var command = new AddNewDiagnosisMedicalRecord(patient.Id, doctorId, notes, illnessId,
                prescribedMedicine, duration);
            var handler = new AddNewDiagnosisMedicalRecordHandler(_patientRepositoryMock.Object,
                _doctorRepositoryMock.Object, _illnessRepositoryMock.Object, _recordRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctorId), Times.Once);
        }

        [Fact]
        public async Task AddNewRegularMedicalRecord_ShouldThrowException_IllnessDoesNotExist()
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

            int doctorId = 3;
            string notes = "Some notes";
            int illnessId = 1;
            string prescribedMedicine = "Ibuprofen";
            int duration = 5;

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patient.Id)).Returns(Task.FromResult(patient));
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctor.Id)).Returns(Task.FromResult(doctor));
            _illnessRepositoryMock.Setup(repo => repo.GetByIdAsync(illnessId));

            var command = new AddNewDiagnosisMedicalRecord(patient.Id, doctorId, notes, illnessId,
                prescribedMedicine, duration);
            var handler = new AddNewDiagnosisMedicalRecordHandler(_patientRepositoryMock.Object,
                _doctorRepositoryMock.Object, _illnessRepositoryMock.Object, _recordRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctorId), Times.Once);
            _illnessRepositoryMock.Verify(repo => repo.GetByIdAsync(illnessId), Times.Once);
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

            var illness = new Illness()
            {
                Id = 1,
                Name = "Flu"
            };

            string notes = "Some notes";
            string prescribedMedicine = "Ibuprofen";
            int duration = 5;

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patient.Id)).Returns(Task.FromResult(patient));
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctor.Id)).Returns(Task.FromResult(doctor));
            _illnessRepositoryMock.Setup(repo => repo.GetByIdAsync(illness.Id)).Returns(Task.FromResult(illness));

            var command = new AddNewDiagnosisMedicalRecord(patient.Id, doctor.Id, notes, illness.Id,
                prescribedMedicine, duration);
            var handler = new AddNewDiagnosisMedicalRecordHandler(_patientRepositoryMock.Object,
                _doctorRepositoryMock.Object, _illnessRepositoryMock.Object, _recordRepositoryMock.Object);

            await Assert.ThrowsAsync<PatientDoctorMisassignationException>(() => handler.Handle(command, default));

            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctor.Id), Times.Once);
            _illnessRepositoryMock.Verify(repo => repo.GetByIdAsync(illness.Id), Times.Once);
        }
    }
}
