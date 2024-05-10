using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Moq;
using Hospital.Application.Doctors.Queries;

namespace Hospital.UnitTests.Doctors.Test.QueryHandlers.Test
{
    public class GetDoctorByIdTest
    {
        private Mock<IRepository<Doctor>> _doctorRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public GetDoctorByIdTest()
        {
            _doctorRepositoryMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task GetDoctorById_ShouldReturnDoctorFullInfoDto()
        {
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
                }
            };

            var doctorDto = new DoctorFullInfoDto()
            {
                Id = 1,
                Name = "Steve",
                Surname = "Smith",
                Address = "Some address",
                PhoneNumber = "068749856",
                Department = "Heart diseases",
                WorkingHours = new DoctorScheduleDto()
                {
                    StartShift = new TimeSpan(7, 0, 0),
                    EndShift = new TimeSpan(16, 0, 0),
                    WeekDays = new List<string>() { "Monday" }
                },
                Patients = new List<PatientShortInfoDto>()
                {
                    new()
                    {
                        FullName = "Michael Bay",
                    }
                }
            };
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctor.Id)).Returns(Task.FromResult(doctor));
            _mapperMock.Setup(mapper => mapper.Map<DoctorFullInfoDto>(doctor))
                .Returns(new DoctorFullInfoDto()
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Surname = doctor.Surname,
                    Address = doctor.Address,
                    PhoneNumber = doctor.PhoneNumber,
                    Department = doctor.Department.Name,
                    WorkingHours = new DoctorScheduleDto()
                    {
                        StartShift = doctor.WorkingHours.StartShift,
                        EndShift = doctor.WorkingHours.EndShift,
                        WeekDays = new List<string>() { "Monday" }
                    },
                    Patients = new List<PatientShortInfoDto>()
                    {
                        new()
                        {
                            FullName = "Michael Bay",
                        }
                    }
                });

            var command = new GetDoctorById(doctor.Id);
            var handler = new GetDoctorByIdHandler(_doctorRepositoryMock.Object, _mapperMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(doctorDto.Id, result.Id);
            Assert.Equal(doctorDto.Patients.Count, result.Patients.Count);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctor.Id), Times.Once);
        }

        [Fact]
        public async Task GetDoctorById_ShouldThrowException_DoctorDoesNotExist()
        {
            int doctorId = 2;
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctorId));

            var command = new GetDoctorById(doctorId);
            var handler = new GetDoctorByIdHandler(_doctorRepositoryMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctorId), Times.Once);
        }
    }
}
