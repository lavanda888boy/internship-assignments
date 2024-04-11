using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Queries;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.Departments.Test.Queries.Test
{
    public class ListAllDepartmentsTest
    {
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;

        public ListAllDepartmentsTest()
        {
            _departmentRepositoryMock = new();
        }

        [Fact]
        public void Handle_ShouldReturnListOfDepartmentDtos()
        {
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "Heart diseases" },
                new Department { Id = 2, Name = "Viruses"},
            };

            _departmentRepositoryMock.Setup(repo => repo.GetAll()).Returns(departments);

            var command = new ListAllDepartments();
            var handler = new ListAllDepartmentsHandler(_departmentRepositoryMock.Object);

            Task<List<DepartmentDto>> departmentDtos = handler.Handle(command, default);

            _departmentRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.Equal(2, departmentDtos.Result.Count);
                Assert.Equal("Heart diseases", departmentDtos.Result[0].Name);
            });
        }
    }
}
