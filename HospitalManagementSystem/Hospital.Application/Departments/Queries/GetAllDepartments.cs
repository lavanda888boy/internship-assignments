using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;
namespace Hospital.Application.Departments.Queries
{
    public record GetAllDepartments() : IRequest<List<DepartmentDto>>;

    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartments, List<DepartmentDto>>
    {
        private readonly IRepository<Department> _departmentRepository;

        public GetAllDepartmentsHandler(IRepository<Department> departmentRepository)
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
