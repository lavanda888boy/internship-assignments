using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Commands
{
    public record DeleteDepartment(int DepartmentId) : IRequest<DepartmentDto>;

    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartment, DepartmentDto>
    {
        private readonly IRepository<Department> _departmentRepository;

        public DeleteDepartmentHandler(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<DepartmentDto> Handle(DeleteDepartment request, CancellationToken cancellationToken)
        {
            var department = _departmentRepository.GetById(request.DepartmentId);

            if (department is null)
            {
                throw new NoEntityFoundException($"Cannot delete non-existing department with id {request.DepartmentId}");
            }
            _departmentRepository.Delete(department);

            return Task.FromResult(DepartmentDto.FromDepartment(department));
        }
    }
}
