using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.MedicalRecords.Test.Commands.Test
{
    public class AdjustRegularMedicalRecordExaminationNotesTest
    {
        private readonly Mock<IRegularMedicalRecordRepository> _medicalRecordRepositoryMock;

        public AdjustRegularMedicalRecordExaminationNotesTest()
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

            var record = new RegularMedicalRecord()
            {
                Id = id,
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes
            };

            _medicalRecordRepositoryMock.Setup(repo => repo.GetById(id)).Returns(record);
            _medicalRecordRepositoryMock.Setup(repo => repo.Update(It.IsAny<RegularMedicalRecord>())).Returns(true);

            var command = new AdjustRegularMedicalRecordExaminationNotes(id, notes);
            var handler = new AdjustRegularMedicalRecordExaminationNotesHandler(_medicalRecordRepositoryMock.Object);

            Task<RegularMedicalRecordDto> recordDto = handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.Equal(id, recordDto.Result.Id);
                Assert.Equal("Seva", recordDto.Result.ExaminedPatient.Name);
                Assert.Equal("Mick", recordDto.Result.ResponsibleDoctor.Name);
                Assert.Equal(notes, recordDto.Result.ExaminationNotes);
            });

            _medicalRecordRepositoryMock.Verify(repo => repo.GetById(id), Times.Once);
            _medicalRecordRepositoryMock.Verify(repo => repo.Update(It.IsAny<RegularMedicalRecord>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "patient feels good")]
        public async Task Handle_ShouldThrowException_RegularMedicalRecordDoesNotExist(int id, string notes)
        {
            _medicalRecordRepositoryMock.Setup(repo => repo.GetById(id));

            var command = new AdjustRegularMedicalRecordExaminationNotes(id, notes);
            var handler = new AdjustRegularMedicalRecordExaminationNotesHandler(_medicalRecordRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _medicalRecordRepositoryMock.Verify(repo => repo.GetById(id), Times.Once);
        }
    }
}
