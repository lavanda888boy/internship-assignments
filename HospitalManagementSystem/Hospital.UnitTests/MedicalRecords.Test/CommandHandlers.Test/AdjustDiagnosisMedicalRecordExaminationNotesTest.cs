using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using Moq;
using Hospital.Application.Abstractions;

namespace Hospital.UnitTests.MedicalRecords.Test.CommandHandlers.Test
{
    public class AdjustDiagnosisMedicalRecordExaminationNotesTest
    {
        private Mock<IRepository<DiagnosisMedicalRecord>> _recordRepositoryMock;

        public AdjustDiagnosisMedicalRecordExaminationNotesTest()
        {
            _recordRepositoryMock = new();
        }

        [Fact]
        public async Task AdjustDiagnosisMedicalRecordExaminationNotes_ShouldReturnUpdatedMedicalRecordDto()
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

            string notes = "Another notes";
            int recordId = 2;

            var record = new DiagnosisMedicalRecord()
            {
                Id = recordId,
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes,
                DiagnosedIllness = new Illness()
                {
                    Name = "Flu"
                },
                ProposedTreatment = new Treatment()
                {
                    PrescribedMedicine = "Panadol",
                    DurationInDays = 5
                }
            };

            _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(recordId)).Returns(Task.FromResult(record));
            _recordRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<DiagnosisMedicalRecord>())).Returns(Task.FromResult(record));

            var command = new AdjustDiagnosisMedicalRecordExaminationNotes(recordId, notes);
            var handler = new AdjustDiagnosisMedicalRecordExaminationNotesHandler(_recordRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(recordId, result);
            _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(recordId), Times.Once);
            _recordRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<DiagnosisMedicalRecord>(r => r.ExaminationNotes == "Another notes")), Times.Once);
        }

        [Fact]
        public async Task AdjustDiagnosisMedicalRecordExaminationNotes_ShouldThrowException_DiagnosisMedicalRecordDoesNotExist()
        {
            int recordId = 2;
            string notes = "Another notes";
            _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(recordId));

            var command = new AdjustDiagnosisMedicalRecordExaminationNotes(recordId, notes);
            var handler = new AdjustDiagnosisMedicalRecordExaminationNotesHandler(_recordRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(recordId), Times.Once);
        }
    }
}
