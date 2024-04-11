using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.Application.Test.MedicalRecords.Test.Commands.Test
{
    public class AdjustDiagnosisMedicalRecordExaminationNotesTest
    {
        private readonly Mock<IDiagnosisMedicalRecordRepository> _medicalRecordRepositoryMock;

        public AdjustDiagnosisMedicalRecordExaminationNotesTest()
        {
            _medicalRecordRepositoryMock = new();
        }

        [Theory]
        [InlineData(1, "patient feels good")]
        public void Handle_ShouldReturnUpdatedMedicalRecordDto(int id, string notes)
        {
            var patient = new Patient()
            {
                Id = 1,
                Name = "Seva",
                Surname = "Bajenov",
                Gender = "M",
                Address = "Dacia 24",
                Age = 20,
                PhoneNumber = "085964712",
                AssignedDoctors = new List<Doctor>
                {
                    new Doctor()
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
                        WorkingHours = new DoctorWorkingHours()
                        {
                            Id = 1,
                            StartShift = new TimeSpan(7, 0, 0),
                            EndShift = new TimeSpan(16, 0, 0),
                            WeekDays = new List<DayOfWeek> {DayOfWeek.Monday},
                        }
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
                WorkingHours = new DoctorWorkingHours()
                {
                    Id = 1,
                    StartShift = new TimeSpan(7, 0, 0),
                    EndShift = new TimeSpan(16, 0, 0),
                    WeekDays = new List<DayOfWeek> { DayOfWeek.Monday },
                }
            };

            var record = new DiagnosisMedicalRecord()
            {
                Id = id,
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes,
                DiagnosedIllness = new Illness()
                {
                    Id = 1,
                    Name = "allergy",
                    IllnessSeverity = IllnessSeverity.MEDIUM,
                },
                ProposedTreatment = new Treatment()
                {
                    Id = 1,
                    PrescribedMedicine = "suprastin",
                    TreatmentDuration = new TimeSpan(4, 0, 0, 0)
                }
            };

            _medicalRecordRepositoryMock.Setup(repo => repo.GetById(id)).Returns(record);
            _medicalRecordRepositoryMock.Setup(repo => repo.Update(It.IsAny<DiagnosisMedicalRecord>())).Returns(true);

            var command = new AdjustDiagnosisMedicalRecordExaminationNotes(id, notes);
            var handler = new AdjustDiagnosisMedicalRecordExaminationNotesHandler(_medicalRecordRepositoryMock.Object);

            Task<DiagnosisMedicalRecordDto> recordDto = handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.Equal(id, recordDto.Result.Id);
                Assert.Equal("Seva", recordDto.Result.ExaminedPatient.Name);
                Assert.Equal("Mick", recordDto.Result.ResponsibleDoctor.Name);
                Assert.Equal(notes, recordDto.Result.ExaminationNotes);
            });

            _medicalRecordRepositoryMock.Verify(repo => repo.GetById(id), Times.Once);
            _medicalRecordRepositoryMock.Verify(repo => repo.Update(It.IsAny<DiagnosisMedicalRecord>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "patient feels good")]
        public async Task Handle_ShouldThrowException_RegularMedicalRecordDoesNotExist(int id, string notes)
        {
            _medicalRecordRepositoryMock.Setup(repo => repo.GetById(id));

            var command = new AdjustDiagnosisMedicalRecordExaminationNotes(id, notes);
            var handler = new AdjustDiagnosisMedicalRecordExaminationNotesHandler(_medicalRecordRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _medicalRecordRepositoryMock.Verify(repo => repo.GetById(id), Times.Once);
        }
    }
}
