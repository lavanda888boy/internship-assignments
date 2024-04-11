using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Queries;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.Doctors.Test.Queries.Test
{
    public class SearchDoctorsByASetOfPropertiesTest
    {
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
        private List<Doctor> _doctors = new()
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
                },
                new Doctor()
                {
                    Id = 2,
                    Name = "Rick",
                    Surname = "Rouse",
                    Address = "Santa Monica",
                    PhoneNumber = "065412389",
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
                        WeekDays = new List<DayOfWeek> {DayOfWeek.Tuesday},
                    }
                }
        };

        public SearchDoctorsByASetOfPropertiesTest()
        {
            _doctorRepositoryMock = new();
        }

        [Theory]
        [MemberData(nameof(SearchDoctorsByASetOfPropertiesData))]
        public void Handle_ShouldReturnListOfDoctorDtos(DoctorFilterDto doctorFilters)
        {
            var filteredDoctors = _doctors.Where(d => d.Name == doctorFilters.Name &&
                                                        d.Department.Name == doctorFilters.DepartmentName).ToList();

            _doctorRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Doctor, bool>>())).Returns(filteredDoctors);

            var command = new SearchDoctorsByASetOfProperties(doctorFilters);
            var handler = new SearchDoctorsByASetOfPropertiesHandler(_doctorRepositoryMock.Object);

            Task<List<DoctorDto>> doctorDtos = handler.Handle(command, default);

            _doctorRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Doctor, bool>>()), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.Single(doctorDtos.Result);
                Assert.Equal("Santa Monica", doctorDtos.Result[0].Address);
                Assert.Equal("065412389", doctorDtos.Result[0].PhoneNumber);
            });
        }

        [Fact]
        public async Task Handle_ShouldThrowException_NoDoctorsWithSuchPropertiesExists()
        {
            DoctorFilterDto doctorFilters = new DoctorFilterDto()
            {
                Surname = "Spouse",
                Address = "Santa Monica"
            };

            var filteredDoctors = _doctors.Where(d => d.Surname == doctorFilters.Surname &&
                                                        d.Address == doctorFilters.Address).ToList();

            _doctorRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Doctor, bool>>())).Returns(filteredDoctors);

            var command = new SearchDoctorsByASetOfProperties(doctorFilters);
            var handler = new SearchDoctorsByASetOfPropertiesHandler(_doctorRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _doctorRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Doctor, bool>>()), Times.Once);
        }

        public static TheoryData<DoctorFilterDto> SearchDoctorsByASetOfPropertiesData => new TheoryData<DoctorFilterDto>()
        {
            {
                new DoctorFilterDto()
                {
                    Name = "Rick",
                    DepartmentName = "Heart diseases"
                }
            }
        };
    }
}
