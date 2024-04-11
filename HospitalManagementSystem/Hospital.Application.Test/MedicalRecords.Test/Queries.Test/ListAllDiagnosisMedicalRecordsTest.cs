using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.Application.Test.MedicalRecords.Test.Queries.Test
{
    public class ListAllDiagnosisMedicalRecordsTest
    {
        private readonly Mock<IDiagnosisMedicalRecordRepository> _recordRepositoryMock;

        public ListAllDiagnosisMedicalRecordsTest()
        {
            _recordRepositoryMock = new();
        }

        [Fact]
        public void Handle_ShouldReturnListOfMedicalRecordDtos()
        {
            Doctor d = new Doctor()
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

            Patient p = new Patient()
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
                        d
                    }
            };

            var records = new List<DiagnosisMedicalRecord>()
                {
                    new DiagnosisMedicalRecord()
                    {
                        Id = 1,
                        ExaminedPatient = p,
                        ResponsibleDoctor = d,
                        DateOfExamination = DateTimeOffset.Now,
                        ExaminationNotes = "patient is not ok",
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
                    }
                };

            _recordRepositoryMock.Setup(repo => repo.GetAll()).Returns(records);

            var command = new ListAllDiagnosisMedicalRecords();
            var handler = new ListAllDiagnosisMedicalRecordsHandler(_recordRepositoryMock.Object);

            Task<List<DiagnosisMedicalRecordDto>> recordDtos = handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.Single(recordDtos.Result);
                Assert.Equal(p.Id, recordDtos.Result[0].Id);
            });
        }
    }
}
