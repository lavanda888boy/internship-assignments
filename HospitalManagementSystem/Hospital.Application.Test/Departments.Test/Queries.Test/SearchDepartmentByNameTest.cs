using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using Moq;
using Hospital.Application.Departments.Queries;
using Hospital.Application.Departments.Responses;

namespace Hospital.Application.Test.Departments.Test.Queries.Test
{
    public class SearchDepartmentByNameTest
    {
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;

        public SearchDepartmentByNameTest()
        {
            _departmentRepositoryMock = new();
        }

        [Theory]
        [InlineData("Heart diseases")]
        public void Handle_ShouldReturnDepartmentDto(string name)
        {
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "Heart diseases" },
                new Department { Id = 2, Name = "Viruses"},
            };

            var filteredDepartments = departments.Where(d => d.Name == name).ToList();

            _departmentRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Department, bool>>())).Returns(filteredDepartments);

            var command = new SearchDepartmentByName(name);
            var handler = new SearchDepartmentByNameHandler(_departmentRepositoryMock.Object);

            Task<DepartmentDto> departmentDto = handler.Handle(command, default);

            _departmentRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Department, bool>>()), Times.Once);
            Assert.Equal("Heart diseases", departmentDto.Result.Name);
        }

        [Theory]
        [InlineData("cancer")]
        public async Task Handle_ShouldThrowException_NoIllnessWithSuchSeverity(string name)
        {
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "Heart diseases" },
                new Department { Id = 2, Name = "Viruses"},
            };

            var filteredDepartments = departments.Where(d => d.Name == name).ToList();

            _departmentRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Department, bool>>())).Returns(filteredDepartments);

            var command = new SearchDepartmentByName(name);
            var handler = new SearchDepartmentByNameHandler(_departmentRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _departmentRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Department, bool>>()), Times.Once);
        }
    }
}
