using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Queries
{
    public record ListAllPaginatedDepartments(int PageNumber = 1, int PageSize = 20) : IRequest<PaginatedResult<DepartmentDto>>;

    public class ListAllPaginatedDepartmentsHandler : IRequestHandler<ListAllPaginatedDepartments, PaginatedResult<DepartmentDto>>
    {
        private readonly IRepository<Department> _departmentRepository;

        public ListAllPaginatedDepartmentsHandler(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<PaginatedResult<DepartmentDto>> Handle(ListAllPaginatedDepartments request, CancellationToken cancellationToken)
        {
            var paginatedDepartments = await _departmentRepository.GetAllPaginatedAsync(request.PageNumber, request.PageSize);
            return await Task.FromResult(new PaginatedResult<DepartmentDto>
            {
                TotalItems = paginatedDepartments.TotalItems,
                Items = paginatedDepartments.Items.Select(DepartmentDto.FromDepartment).ToList()
            });
        }
    }
}
