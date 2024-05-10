using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Queries;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.UnitTests.Doctors.Test.QueryHandlers.Test
{
    public class ListAllDoctorsTest
    {
        private Mock<IRepository<Doctor>> _doctorRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public ListAllDoctorsTest()
        {
            _doctorRepositoryMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task ListAllDoctors_ShouldReturnListOfDoctorFullInfoDto()
        {
            var doctors = new List<Doctor>()
            {
                new() {
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
                    }
                }
            };

            _doctorRepositoryMock.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(doctors));
            _mapperMock.Setup(mapper => mapper.Map<List<DoctorFullInfoDto>>(doctors))
                .Returns(new List<DoctorFullInfoDto>()
                {
                    new()
                    {
                        Id = doctors[0].Id,
                        Name = doctors[0].Name,
                        Surname = doctors[0].Surname,
                        Address = doctors[0].Address,
                        PhoneNumber = doctors[0].PhoneNumber,
                        Department = doctors[0].Department.Name,
                        WorkingHours = new DoctorScheduleDto()
                        {
                            StartShift = doctors[0].WorkingHours.StartShift,
                            EndShift = doctors[0].WorkingHours.EndShift,
                            WeekDays = new List<string>() { "Monday" }
                        },
                        Patients = new List<PatientShortInfoDto>()
                        {
                            new()
                            {
                                FullName = "Michael Bay",
                            }
                        }
                    }
                });

            var command = new ListAllDoctors();
            var handler = new ListAllDoctorsHandler(_doctorRepositoryMock.Object, _mapperMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(doctors.Count, result.Count);
            Assert.Equal(doctors[0].Id, result[0].Id);
            _doctorRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}
