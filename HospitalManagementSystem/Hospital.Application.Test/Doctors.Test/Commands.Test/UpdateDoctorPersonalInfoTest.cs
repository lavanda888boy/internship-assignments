using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Commands;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.Doctors.Test.Commands.Test
{
    public class UpdateDoctorPersonalInfoTest
    {
        private Mock<IDoctorRepository> _doctorRepositoryMock;
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;

        public UpdateDoctorPersonalInfoTest()
        {
            _departmentRepositoryMock = new();
            _doctorRepositoryMock = new();
        }

        [Theory]
        [MemberData(nameof(UpdateDoctorPersonalInfoData))]
        public void Handle_ShouldReturnNewlyEmployedDoctorDto(Doctor doctor)
        {
            _doctorRepositoryMock.Setup(repo => repo.GetById(doctor.Id)).Returns(doctor);
            _departmentRepositoryMock.Setup(repo => repo.GetById(doctor.Department.Id)).Returns(doctor.Department);
            _doctorRepositoryMock.Setup(repo => repo.Update(It.IsAny<Doctor>())).Returns(true);

            var command = new UpdateDoctorPersonalInfo(doctor.Id, doctor.Name, doctor.Surname,
                doctor.Address, doctor.PhoneNumber, doctor.Department.Id, doctor.WorkingHours.Id,
                doctor.WorkingHours.StartShift, doctor.WorkingHours.EndShift, doctor.WorkingHours.WeekDays);
            var handler = new UpdateDoctorPersonalInfoHandler(_doctorRepositoryMock.Object,
                _departmentRepositoryMock.Object);

            Task<DoctorDto> doctorDto = handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.Equal(doctor.Name, doctorDto.Result.Name);
                Assert.Equal(doctor.Address, doctorDto.Result.Address);
                Assert.Equal(doctor.PhoneNumber, doctorDto.Result.PhoneNumber);
                Assert.Equal(doctor.WorkingHours.Id, doctorDto.Result.WorkingHours.Id);
            });

            _doctorRepositoryMock.Verify(repo => repo.GetById(doctor.Id), Times.Once);
            _departmentRepositoryMock.Verify(repo => repo.GetById(doctor.Department.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.Update(It.IsAny<Doctor>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UpdateDoctorPersonalInfoData))]
        public async Task Handle_ShouldThrowException_DoctorDoesNotExist(Doctor doctor)
        {
            _doctorRepositoryMock.Setup(repo => repo.GetById(doctor.Id));

            var command = new UpdateDoctorPersonalInfo(doctor.Id, doctor.Name, doctor.Surname,
                doctor.Address, doctor.PhoneNumber, doctor.Department.Id, doctor.WorkingHours.Id,
                doctor.WorkingHours.StartShift, doctor.WorkingHours.EndShift, doctor.WorkingHours.WeekDays);
            var handler = new UpdateDoctorPersonalInfoHandler(_doctorRepositoryMock.Object,
                _departmentRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _doctorRepositoryMock.Verify(repo => repo.GetById(doctor.Id), Times.Once);
        }

        public static TheoryData<Doctor> UpdateDoctorPersonalInfoData => new TheoryData<Doctor>()
        {
            {
                new Doctor()
                {
                    Id = 1,
                    Name = "Mick",
                    Surname = "Mouse",
                    Address = "Moscow",
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
    }
}
