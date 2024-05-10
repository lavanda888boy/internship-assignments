using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.UnitTests.MedicalRecords.Test.CommandHandlers.Test
{
    public class AdjustRegularMedicalRecordExaminationNotesTest
    {
        private Mock<IRepository<RegularMedicalRecord>> _recordRepositoryMock;

        public AdjustRegularMedicalRecordExaminationNotesTest()
        {
            _recordRepositoryMock = new();
        }

        [Fact]
        public async Task AdjustRegularMedicalRecordsExaminationNotes_ShouldReturnUpdatedRegularMedicalRecordId()
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

            var record = new RegularMedicalRecord()
            {
                Id = recordId,
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes
            };

            _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(recordId)).Returns(Task.FromResult(record));
            _recordRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<RegularMedicalRecord>())).Returns(Task.FromResult(record));

            var command = new AdjustRegularMedicalRecordExaminationNotes(recordId, notes);
            var handler = new AdjustRegularMedicalRecordExaminationNotesHandler(_recordRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(recordId, result);
            _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(recordId), Times.Once);
            _recordRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<RegularMedicalRecord>(r => r.ExaminationNotes == "Another notes")), Times.Once);
        }

        [Fact]
        public async Task AdjustRegularMedicalRecordsExaminationNotes_ShouldThrowException_RegularMedicalRecordDoesNotExist()
        {
            int recordId = 2;
            string notes = "Another notes";
            _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(recordId));

            var command = new AdjustRegularMedicalRecordExaminationNotes(recordId, notes);
            var handler = new AdjustRegularMedicalRecordExaminationNotesHandler(_recordRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(recordId), Times.Once);
        }
    }
}
