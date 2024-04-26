using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Queries
{
    public record SearchDepartmentByName(string DepartmentName) : IRequest<DepartmentDto>;

    public class SearchDepartmentByNameHandler : IRequestHandler<SearchDepartmentByName, DepartmentDto>
    {
        private readonly IRepository<Department> _departmentRepository;

        public SearchDepartmentByNameHandler(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentDto> Handle(SearchDepartmentByName request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.SearchByPropertyAsync(d => d.Name == request.DepartmentName);

            if (departments.Count == 0)
            {
                throw new NoEntityFoundException("No department with such name exists");
            }

            return await Task.FromResult(DepartmentDto.FromDepartment(departments[0]));
        }
    }
}
