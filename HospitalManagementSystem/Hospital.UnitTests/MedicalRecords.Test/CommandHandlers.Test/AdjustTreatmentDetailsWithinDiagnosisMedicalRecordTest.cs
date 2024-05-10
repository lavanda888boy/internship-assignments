using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using Moq;
using Hospital.Application.Abstractions;

namespace Hospital.UnitTests.MedicalRecords.Test.CommandHandlers.Test
{
    public class AdjustTreatmentDetailsWithinDiagnosisMedicalRecordTest
    {
        private readonly Mock<IRepository<DiagnosisMedicalRecord>> _recordRepositoryMock;
        private readonly Mock<IRepository<Illness>> _illnessRepositoryMock;
        private readonly Mock<IRepository<Treatment>> _treatmentRepositoryMock;

        public AdjustTreatmentDetailsWithinDiagnosisMedicalRecordTest()
        {
            _recordRepositoryMock = new();
            _illnessRepositoryMock = new();
            _treatmentRepositoryMock = new();
        }

        [Fact]
        public async Task AdjustTreatmentDetailsWithinDiagnosisMedicalRecord_ShouldReturnUpdatedMedicalRecordDto()
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

            _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(record.Id)).Returns(Task.FromResult(record));
            _recordRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<DiagnosisMedicalRecord>()));
            _illnessRepositoryMock.Setup(repo => repo.GetByIdAsync(record.DiagnosedIllness.Id)).Returns(Task.FromResult(record.DiagnosedIllness));

            var command = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(record.Id, record.DiagnosedIllness.Id, "Ibuprofen", record.ProposedTreatment.DurationInDays);
            var handler = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler(_recordRepositoryMock.Object,
                _illnessRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(record.Id, result);
            _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(record.Id), Times.Once);
            _recordRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<DiagnosisMedicalRecord>(r =>
                r.ProposedTreatment.PrescribedMedicine == "Ibuprofen"
            )), Times.Once);
            _illnessRepositoryMock.Verify(repo => repo.GetByIdAsync(record.DiagnosedIllness.Id), Times.Once);
        }

        [Fact]
        public async Task AdjustTreatmentDetailsWithinDiagnosisMedicalRecord_ShouldThrowException_MedicalRecordDoesNotExist()
        {
            int recordId = 2;
            int illnessId = 1;
            string prescribedMedicine = "Ibuprofen";
            int duration = 5;
            _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(recordId));

            var command = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(recordId, illnessId, prescribedMedicine, duration);
            var handler = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler(_recordRepositoryMock.Object,
                _illnessRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(recordId), Times.Once);
        }

        [Fact]
        public async Task AdjustTreatmentDetailsWithinDiagnosisMedicalRecord_ShouldThrowException_IllnessDoesNotExist()
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

            _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(record.Id)).Returns(Task.FromResult(record));
            _illnessRepositoryMock.Setup(repo => repo.GetByIdAsync(record.DiagnosedIllness.Id));

            var command = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(record.Id, record.DiagnosedIllness.Id,
                record.ProposedTreatment.PrescribedMedicine, record.ProposedTreatment.DurationInDays);
            var handler = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler(_recordRepositoryMock.Object,
                _illnessRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(record.Id), Times.Once);
            _illnessRepositoryMock.Verify(repo => repo.GetByIdAsync(record.DiagnosedIllness.Id), Times.Once);
        }
    }
}
