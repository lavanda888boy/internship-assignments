using Hospital.Application.Abstractions;
using Hospital.Domain.Models;
using Moq;
using Hospital.Application.Departments.Commands;
using Hospital.Application.Departments.Responses;

namespace Hospital.Application.Test.Departments.Test.Commands.Test
{
    public class RegisterNewHospitalDepartmentTest
    {
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;

        public RegisterNewHospitalDepartmentTest()
        {
            _departmentRepositoryMock = new();
        }

        [Theory]
        [InlineData(1, "Heart diseases")]
        public void Handle_ShouldReturnNewlyRegisteredDepartment(int id, string name)
        {
            var department = new Department()
            {
                Id = id,
                Name = name,
            };

            _departmentRepositoryMock.Setup(repo => repo.Create(It.IsAny<Department>())).Returns(department);

            var command = new RegisterNewHospitalDepartment(id, name);
            var handler = new RegisterNewHospitalDepartmentHandler(_departmentRepositoryMock.Object);

            Task<DepartmentDto> departmentDto = handler.Handle(command, default);

            _departmentRepositoryMock.Verify(repo => repo.Create(It.IsAny<Department>()), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.Equal(id, departmentDto.Result.Id);
                Assert.Equal(name, departmentDto.Result.Name);
            });
        }
    }
}
