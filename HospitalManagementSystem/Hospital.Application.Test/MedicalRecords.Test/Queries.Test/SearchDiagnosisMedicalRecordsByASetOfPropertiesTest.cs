using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.Application.Test.MedicalRecords.Test.Queries.Test
{
    public class SearchDiagnosisMedicalRecordsByASetOfPropertiesTest
    {
        private readonly Mock<IDiagnosisMedicalRecordRepository> _medicalRecordRepositoryMock;
        private List<DiagnosisMedicalRecord> _records = new()
        {
            new DiagnosisMedicalRecord()
            {
                Id = 1,
                ExaminedPatient = new Patient()
                {
                    Id = 1,
                    Name = "Seva",
                    Surname = "Bajenov",
                    Gender = "M",
                    Address = "Dacia 24",
                    Age = 20,
                    PhoneNumber = "085964712",
                },
                ResponsibleDoctor = new Doctor()
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
                },
                DateOfExamination = DateTimeOffset.UtcNow,
                ExaminationNotes = "some notes",
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
            },
            new DiagnosisMedicalRecord()
            {
                Id = 2,
                ExaminedPatient = new Patient()
                {
                    Id = 8,
                    Name = "Seva",
                    Surname = "Bajenov",
                    Gender = "M",
                    Address = "Dacia 24",
                    Age = 20,
                    PhoneNumber = "085964712",
                },
                ResponsibleDoctor = new Doctor()
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
                },
                DateOfExamination = DateTimeOffset.UtcNow,
                ExaminationNotes = "some notes",
                DiagnosedIllness = new Illness()
                {
                    Id = 2,
                    Name = "flu",
                    IllnessSeverity = IllnessSeverity.MEDIUM,
                },
                ProposedTreatment = new Treatment()
                {
                    Id = 2,
                    PrescribedMedicine = "panadol",
                    TreatmentDuration = new TimeSpan(4, 0, 0, 0)
                }
            },
        };

        public SearchDiagnosisMedicalRecordsByASetOfPropertiesTest()
        {
            _medicalRecordRepositoryMock = new();
        }

        [Theory]
        [MemberData(nameof(SearchDiagnosisMedicalRecordsByASetOfPropertiesData))]
        public void Handle_ShouldReturnListOfDiagnosisMedicalRecordDtos(DiagnosisMedicalRecordFilterDto recordFilters)
        {
            var filteredRecords = _records.Where(r => r.DiagnosedIllness.Name == recordFilters.DiagnosedIllnessName).ToList();

            _medicalRecordRepositoryMock.Setup(repo => repo.SearchByProperty(
                It.IsAny<Func<DiagnosisMedicalRecord, bool>>())).Returns(filteredRecords);

            var command = new SearchDiagnosisMedicalRecordsByASetOfProperties(recordFilters);
            var handler = new SearchDiagnosisMedicalRecordsByASetOfPropertiesHandler(_medicalRecordRepositoryMock.Object);

            Task<List<DiagnosisMedicalRecordDto>> recordDtos = handler.Handle(command, default);

            _medicalRecordRepositoryMock.Verify(repo => repo.SearchByProperty(
                It.IsAny<Func<DiagnosisMedicalRecord, bool>>()), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.Single(recordDtos.Result);
                Assert.Equal(1, recordDtos.Result[0].ExaminedPatient.Id);
            });
        }

        [Fact]
        public async Task Handle_ShouldThrowException_NoDiagnosisMedicalRecordsWithSuchPropertiesExists()
        {
            DiagnosisMedicalRecordFilterDto recordFilters = new DiagnosisMedicalRecordFilterDto()
            {
                DiagnosedIllnessName = "allergy",
                PrescribedMedicine = "panadol"
            };

            var filteredRecords = _records.Where(r => r.DiagnosedIllness.Name == recordFilters.DiagnosedIllnessName &&
                                                        r.ProposedTreatment.PrescribedMedicine == recordFilters.PrescribedMedicine).ToList();

            _medicalRecordRepositoryMock.Setup(repo => repo.SearchByProperty(
                It.IsAny<Func<DiagnosisMedicalRecord, bool>>())).Returns(filteredRecords);

            var command = new SearchDiagnosisMedicalRecordsByASetOfProperties(recordFilters);
            var handler = new SearchDiagnosisMedicalRecordsByASetOfPropertiesHandler(_medicalRecordRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _medicalRecordRepositoryMock.Verify(repo => repo.SearchByProperty(
                It.IsAny<Func<DiagnosisMedicalRecord, bool>>()), Times.Once);
        }

        public static TheoryData<DiagnosisMedicalRecordFilterDto> SearchDiagnosisMedicalRecordsByASetOfPropertiesData
            => new TheoryData<DiagnosisMedicalRecordFilterDto>()
        {
            {
                new DiagnosisMedicalRecordFilterDto()
                {
                    DiagnosedIllnessName = "allergy"
                }
            }
        };
    }
}
