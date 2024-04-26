using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;
namespace Hospital.Application.Departments.Queries
{
    public record ListAllDepartments() : IRequest<List<DepartmentDto>>;

    public class ListAllDepartmentsHandler : IRequestHandler<ListAllDepartments, List<DepartmentDto>>
    {
        private readonly IRepository<Department> _departmentRepository;

        public ListAllDepartmentsHandler(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<DepartmentDto>> Handle(ListAllDepartments request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.GetAllAsync();
            return await Task.FromResult(departments.Select(DepartmentDto.FromDepartment).ToList());
        }
    }
}
