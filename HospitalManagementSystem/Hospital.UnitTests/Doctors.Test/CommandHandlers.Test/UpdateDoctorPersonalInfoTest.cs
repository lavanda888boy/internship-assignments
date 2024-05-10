using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Commands;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.UnitTests.Doctors.Test.CommandHandlers.Test
{
    public class UpdateDoctorPersonalInfoTest
    {
        private Mock<IRepository<Doctor>> _doctorRepositoryMock;
        private Mock<IRepository<Department>> _departmentRepositoryMock;

        public UpdateDoctorPersonalInfoTest()
        {
            _doctorRepositoryMock = new();
            _departmentRepositoryMock = new();
        }

        [Fact]
        public async Task UpdateDoctorPersonalInfo_ShouldReturnUpdatedDoctorId()
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

            var department = new Department()
            {
                Id = 2,
                Name = "Lung diseases",
            };

            _departmentRepositoryMock.Setup(repo => repo.GetByIdAsync(2)).Returns(Task.FromResult(department));
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).Returns(Task.FromResult(doctor));
            _doctorRepositoryMock.Setup(repo => repo.UpdateAsync(doctor));

            var command = new UpdateDoctorPersonalInfo(doctor.Id, doctor.Name, doctor.Surname, "another address",
                doctor.PhoneNumber, 2, doctor.WorkingHours.StartShift, doctor.WorkingHours.EndShift,
                new List<int>() { 1, 2, 3 });
            var handler = new UpdateDoctorPersonalInfoHandler(_doctorRepositoryMock.Object, _departmentRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(doctor.Id, result);
            _departmentRepositoryMock.Verify(repo => repo.GetByIdAsync(2), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctor.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Doctor>(d => d.Department.Id == 2)), Times.Once);
        }

        [Fact]
        public async Task UpdateDoctorPersonalInfo_ShouldThrowException_UpdatedDepartmentDoesNotExist()
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

            _departmentRepositoryMock.Setup(repo => repo.GetByIdAsync(2));

            var command = new UpdateDoctorPersonalInfo(doctor.Id, doctor.Name, doctor.Surname, "another address",
                doctor.PhoneNumber, 2, doctor.WorkingHours.StartShift, doctor.WorkingHours.EndShift,
                new List<int>() { 1, 2, 3 });
            var handler = new UpdateDoctorPersonalInfoHandler(_doctorRepositoryMock.Object, _departmentRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _departmentRepositoryMock.Verify(repo => repo.GetByIdAsync(2), Times.Once);
        }

        [Fact]
        public async Task UpdateDoctorPersonalInfo_ShouldThrowException_DoctorToUpdateDoesNotExist()
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

            var department = new Department()
            {
                Id = 2,
                Name = "Lung diseases",
            };

            _departmentRepositoryMock.Setup(repo => repo.GetByIdAsync(2)).Returns(Task.FromResult(department));
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(1));

            var command = new UpdateDoctorPersonalInfo(doctor.Id, doctor.Name, doctor.Surname, "another address",
                doctor.PhoneNumber, 2, doctor.WorkingHours.StartShift, doctor.WorkingHours.EndShift, 
                new List<int>() { 1, 2, 3 });
            var handler = new UpdateDoctorPersonalInfoHandler(_doctorRepositoryMock.Object, _departmentRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _departmentRepositoryMock.Verify(repo => repo.GetByIdAsync(2), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }
    }
}
