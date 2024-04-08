using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using MediatR;
namespace Hospital.Application.Departments.Queries
{
    public record ListAllDepartments() : IRequest<List<DepartmentDto>>;

    public class ListAllDepartmentsHandler : IRequestHandler<ListAllDepartments, List<DepartmentDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public ListAllDepartmentsHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<List<DepartmentDto>> Handle(ListAllDepartments request, CancellationToken cancellationToken)
        {
            var departments = _departmentRepository.GetAll();
            return Task.FromResult(departments.Select(DepartmentDto.FromDepartment).ToList());
        }
    }
}
