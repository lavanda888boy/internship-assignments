using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using MediatR;

namespace Hospital.Application.Departments.Commands
{
    public record DeleteDepartment(int DepartmentId) : IRequest<int>;

    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartment, int>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DeleteDepartmentHandler(IDepartmentRepository departmentRepository)
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
