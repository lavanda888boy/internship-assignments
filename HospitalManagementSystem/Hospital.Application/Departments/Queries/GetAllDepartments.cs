using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using MediatR;
namespace Hospital.Application.Departments.Queries
{
    public record GetAllDepartments() : IRequest<List<DepartmentDto>>;

    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartments, List<DepartmentDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetAllDepartmentsHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<List<DepartmentDto>> Handle(GetAllDepartments request, CancellationToken cancellationToken)
        {
            var departments = _departmentRepository.GetAll();
            return Task.FromResult(departments.Select(DepartmentDto.FromDepartment).ToList());
        }
    }
}
