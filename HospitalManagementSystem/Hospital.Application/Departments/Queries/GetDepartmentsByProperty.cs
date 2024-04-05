using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Queries
{
    public record GetDepartmentsByProperty(Func<Department, bool> DepartmentProperty) 
        : IRequest<List<DepartmentDto>>;

    public class GetDepartmentsByPropertyHandler : IRequestHandler<GetDepartmentsByProperty,
        List<DepartmentDto>>
    {
        private readonly IRepository<Department> _departmentRepository;

        public GetDepartmentsByPropertyHandler(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<List<DepartmentDto>> Handle(GetDepartmentsByProperty request, CancellationToken cancellationToken)
        {
            var departments = _departmentRepository.GetByProperty(request.DepartmentProperty);

            if (departments is null)
            {
                throw new NoEntityFoundException("No departments with such properties exist");
            }

            return Task.FromResult(departments.Select(DepartmentDto.FromDepartment).ToList());
        }
    }
}
