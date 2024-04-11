using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Commands;
using Hospital.Application.Departments.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.Departments.Test.Commands.Test
{
    public class ChangeDepartmentClassificationTest
    {
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;

        public ChangeDepartmentClassificationTest()
        {
            _departmentRepositoryMock = new();
        }

        [Theory]
        [InlineData(1, "Heart diseases")]
        public void Handle_ShouldReturnUpdatedDepartment(int id, string name)
        {
            var department = new Department()
            {
                Id = id,
                Name = name,
            };

            _departmentRepositoryMock.Setup(repo => repo.Update(It.IsAny<Department>())).Returns(true);
            _departmentRepositoryMock.Setup(repo => repo.GetById(id)).Returns(department);

            var command = new ChangeDepartmentClassification(id, name);
            var handler = new ChangeDepartmentClassificationHandler(_departmentRepositoryMock.Object);

            Task<DepartmentDto> departmentDto = handler.Handle(command, default);

            _departmentRepositoryMock.Verify(repo => repo.Update(It.IsAny<Department>()), Times.Once);
            _departmentRepositoryMock.Verify(repo => repo.GetById(id), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.Equal(id, departmentDto.Result.Id);
                Assert.Equal(name, departmentDto.Result.Name);
            });
        }

        [Theory]
        [InlineData(2, "Heart diseases")]
        public async Task Handle_ShouldThrowNoEntityFoundException(int id, string name)
        {
            var department = new Department()
            {
                Id = id,
                Name = name,
            };

            _departmentRepositoryMock.Setup(repo => repo.Update(It.IsAny<Department>())).Returns(false);

            var command = new ChangeDepartmentClassification(id, name);
            var handler = new ChangeDepartmentClassificationHandler(_departmentRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _departmentRepositoryMock.Verify(repo => repo.Update(It.IsAny<Department>()), Times.Once);
        }
    }
}
