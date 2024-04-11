using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Queries;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.Patients.Test.Queries.Test
{
    public class SearchPatientsByASetOfPropertiesTest
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private List<Patient> _patients = new()
        {
            new Patient()
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
                },
                new Patient()
                {
                    Id = 2,
                    Name = "Seva",
                    Surname = "Smith",
                    Gender = "M",
                    Address = "California",
                    Age = 35,
                    PhoneNumber = "045698745",
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
                }
        };

        public SearchPatientsByASetOfPropertiesTest()
        {
            _patientRepositoryMock = new();
        }

        [Theory]
        [MemberData(nameof(SearchPatientsByASetOfPropertiesData))]
        public void Handle_ShouldReturnListOfPatientDtos(PatientFilterDto patientFilters)
        {
            var filteredPatients = _patients.Where(p => p.Name == patientFilters.Name &&
                                                        p.Gender == patientFilters.Gender).ToList();

            _patientRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Patient, bool>>())).Returns(filteredPatients);

            var command = new SearchPatientsByASetOfProperties(patientFilters);
            var handler = new SearchPatientsByASetOfPropertiesHandler(_patientRepositoryMock.Object);

            Task<List<PatientDto>> patientDtos = handler.Handle(command, default);

            _patientRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Patient, bool>>()), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.Equal(2, patientDtos.Result.Count);
                Assert.Equal("Bajenov", patientDtos.Result[0].Surname);
                Assert.Equal("Smith", patientDtos.Result[1].Surname);
            });
        }

        [Fact]
        public async Task Handle_ShouldThrowException_NoPatientsWithSuchPropertiesExists()
        {
            PatientFilterDto patientFilters = new PatientFilterDto()
            {
                Age = 35,
                Gender = "F"
            };

            var filteredPatients = _patients.Where(p => p.Name == patientFilters.Name &&
                                                        p.Gender == patientFilters.Gender).ToList();

            _patientRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Patient, bool>>())).Returns(filteredPatients);

            var command = new SearchPatientsByASetOfProperties(patientFilters);
            var handler = new SearchPatientsByASetOfPropertiesHandler(_patientRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Patient, bool>>()), Times.Once);
        }

        public static TheoryData<PatientFilterDto> SearchPatientsByASetOfPropertiesData => new TheoryData<PatientFilterDto>()
        {
            {
                new PatientFilterDto()
                {
                    Name = "Seva",
                    Gender = "M",
                }
            }
        };
    }
}
