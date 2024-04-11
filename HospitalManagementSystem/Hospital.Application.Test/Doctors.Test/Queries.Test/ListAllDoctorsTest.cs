using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Queries;
using Hospital.Application.Doctors.Responses;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.Doctors.Test.Queries.Test
{
    public class ListAllDoctorsTest
    {
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

        public ListAllDoctorsTest()
        {
            _doctorRepositoryMock = new();
        }

        [Fact]
        public void Handle_ShouldReturnListOfDoctorDtos()
        {
            var doctors = new List<Doctor>()
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

            _doctorRepositoryMock.Setup(repo => repo.GetAll()).Returns(doctors);

            var command = new ListAllDoctors();
            var handler = new ListAllDoctorsHandler(_doctorRepositoryMock.Object);

            Task<List<DoctorDto>> doctorDtos = handler.Handle(command, default);

            _doctorRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.Equal(2, doctorDtos.Result.Count);
                Assert.Equal("Chisinau", doctorDtos.Result[0].Address);
                Assert.Equal("Rick", doctorDtos.Result[1].Name);
            });
        }
    }
}
