using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.MedicalRecords.Test.Queries.Test
{
    public class SearchRegularMedicalRecordsByASetOfPropertiesTest
    {
        private readonly Mock<IRegularMedicalRecordRepository> _medicalRecordRepositoryMock;
        private List<RegularMedicalRecord> _records = new()
        {
            new RegularMedicalRecord()
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
                ExaminationNotes = "some notes"
            },
            new RegularMedicalRecord()
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
                ExaminationNotes = "some notes"
            },
        };

        public SearchRegularMedicalRecordsByASetOfPropertiesTest()
        {
            _medicalRecordRepositoryMock = new();
        }

        [Theory]
        [MemberData(nameof(SearchRegularMedicalRecordsByASetOfPropertiesData))]
        public void Handle_ShouldReturnListOfRegularMedicalRecordDtos(RegularMedicalRecordFilterDto recordFilters)
        {
            var filteredRecords = _records.Where(r => r.ExaminedPatient.Id == recordFilters.ExaminedPatientId).ToList();

            _medicalRecordRepositoryMock.Setup(repo => repo.SearchByProperty(
                It.IsAny<Func<RegularMedicalRecord, bool>>())).Returns(filteredRecords);

            var command = new SearchRegularMedicalRecordsByASetProperties(recordFilters);
            var handler = new SearchRegularMedicalRecordsByASetPropertiesHandler(_medicalRecordRepositoryMock.Object);

            Task<List<RegularMedicalRecordDto>> recordDtos = handler.Handle(command, default);

            _medicalRecordRepositoryMock.Verify(repo => repo.SearchByProperty(
                It.IsAny<Func<RegularMedicalRecord, bool>>()), Times.Once);
            
            Assert.Multiple(() =>
            {
                Assert.Single(recordDtos.Result);
                Assert.Equal(1, recordDtos.Result[0].ExaminedPatient.Id);
            });
        }

        [Fact]
        public async Task Handle_ShouldThrowException_NoRegularMedicalRecordsWithSuchPropertiesExists()
        {
            RegularMedicalRecordFilterDto recordFilters = new RegularMedicalRecordFilterDto()
            {
                ExaminedPatientId = 1,
                ResponsibleDoctorId = 5
            };

            var filteredRecords = _records.Where(r => r.ExaminedPatient.Id == recordFilters.ExaminedPatientId &&
                                                        r.ResponsibleDoctor.Id == recordFilters.ResponsibleDoctorId).ToList();

            _medicalRecordRepositoryMock.Setup(repo => repo.SearchByProperty(
                It.IsAny<Func<RegularMedicalRecord, bool>>())).Returns(filteredRecords);

            var command = new SearchRegularMedicalRecordsByASetProperties(recordFilters);
            var handler = new SearchRegularMedicalRecordsByASetPropertiesHandler(_medicalRecordRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _medicalRecordRepositoryMock.Verify(repo => repo.SearchByProperty(
                It.IsAny<Func<RegularMedicalRecord, bool>>()), Times.Once);
        }

        public static TheoryData<RegularMedicalRecordFilterDto> SearchRegularMedicalRecordsByASetOfPropertiesData 
            => new TheoryData<RegularMedicalRecordFilterDto>()
        {
            {
                new RegularMedicalRecordFilterDto()
                {
                    ExaminedPatientId = 1
                }
            }
        };
    }
}
