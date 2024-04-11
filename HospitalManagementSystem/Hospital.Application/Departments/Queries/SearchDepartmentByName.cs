using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Application.Exceptions;
using MediatR;

namespace Hospital.Application.Departments.Queries
{
    public record SearchDepartmentByName(string DepartmentName) : IRequest<DepartmentDto>;

    public class SearchDepartmentByNameHandler : IRequestHandler<SearchDepartmentByName, DepartmentDto>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public SearchDepartmentByNameHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<DepartmentDto> Handle(SearchDepartmentByName request, CancellationToken cancellationToken)
        {
            var departments = _departmentRepository.SearchByProperty(d => d.Name == request.DepartmentName);

            if (departments.Count == 0)
            {
                throw new NoEntityFoundException("No department with such name exists");
            }

            return Task.FromResult(DepartmentDto.FromDepartment(departments[0]));
        }
    }
}
