using Hospital.Domain.Models;

namespace Hospital.Application.Departments.Responses
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public static DepartmentDto FromDepartment(Department department)
        {
            return new DepartmentDto()
            {
                Id = department.Id,
                Name = department.Name,
            };
        }
    }
}
