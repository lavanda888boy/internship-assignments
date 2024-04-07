using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Commands
{
    public record DeleteDepartment(int DepartmentId) : IRequest<int>;

    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartment, int>
    {
        private readonly IRepository<Department> _departmentRepository;

        public DeleteDepartmentHandler(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<int> Handle(DeleteDepartment request, CancellationToken cancellationToken)
        {
            var result = _departmentRepository.Delete(request.DepartmentId);

            if (result)
            {
                return Task.FromResult(request.DepartmentId);
            }
            else
            {
                throw new NoEntityFoundException($"Cannot delete non-existing department with id {request.DepartmentId}");
            }
        }
    }
}
