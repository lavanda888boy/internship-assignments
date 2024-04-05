using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Queries
{
    public record GetDepartmentById(int DepartmentId) : IRequest<DepartmentDto>;

    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentById, DepartmentDto>
    {
        private readonly IRepository<Department> _departmentRepository;

        public GetDepartmentByIdHandler(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<DepartmentDto> Handle(GetDepartmentById request, CancellationToken cancellationToken)
        {
            var department = _departmentRepository.GetById(request.DepartmentId);

            if (department is null)
            {
                throw new NoEntityFoundException($"There is no department with id {request.DepartmentId}");
            }

            return Task.FromResult(DepartmentDto.FromDepartment(department));
        }
    }
}
