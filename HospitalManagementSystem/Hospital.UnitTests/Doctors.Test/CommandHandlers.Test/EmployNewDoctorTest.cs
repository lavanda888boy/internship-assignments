using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using Moq;
using Hospital.Application.Doctors.Commands;

namespace Hospital.UnitTests.Doctors.Test.CommandHandlers.Test
{
    public class EmployNewDoctorTest
    {
        private Mock<IRepository<Doctor>> _doctorRepositoryMock;
        private Mock<IRepository<Department>> _departmentRepositoryMock;

        public EmployNewDoctorTest()
        {
            _doctorRepositoryMock = new();
            _departmentRepositoryMock = new();
        }

        [Fact]
        public async Task EmployNewDoctor_ShouldReturnDoctorId()
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

            _doctorRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Doctor>())).Returns(Task.FromResult(doctor));
            _departmentRepositoryMock.Setup(repo => repo.GetByIdAsync(doctor.Department.Id)).Returns(Task.FromResult(doctor.Department));

            var command = new EmployNewDoctor(doctor.Name, doctor.Surname, doctor.Address, doctor.PhoneNumber, 1, new TimeSpan(7, 0, 0),
                new TimeSpan(16, 0, 0), new List<int>() { 1, 2, 3, 4 });
            var handler = new EmployNewDoctorHandler(_doctorRepositoryMock.Object, _departmentRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(doctor.Id, result);
            _departmentRepositoryMock.Verify(repo => repo.GetByIdAsync(doctor.Department.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Doctor>()), Times.Once);
        }

        [Fact]
        public async Task EmployNewDoctor_ShouldThrowException_DepartmentDoesNotExist()
        {
            int departmentId = 1;
            _departmentRepositoryMock.Setup(repo => repo.GetByIdAsync(departmentId));

            var command = new EmployNewDoctor("Bill", "Murray", "address", "089745639", 1, new TimeSpan(18, 0, 0),
                new TimeSpan(2, 0, 0), new List<int>() { 1, 2, 3, 4 });
            var handler = new EmployNewDoctorHandler(_doctorRepositoryMock.Object, _departmentRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _departmentRepositoryMock.Verify(repo => repo.GetByIdAsync(departmentId), Times.Once);
        }
    }
}
