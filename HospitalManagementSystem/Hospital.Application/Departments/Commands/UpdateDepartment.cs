using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Commands
{
    public record UpdateDepartment(int Id, string Name) : IRequest<DepartmentDto>;

    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartment, DepartmentDto>
    {
        private readonly IRepository<Department> _departmentRepository;

        public UpdateDepartmentHandler(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<DepartmentDto> Handle(UpdateDepartment request, CancellationToken cancellationToken)
        {
            var result = _departmentRepository.Update(new Department()
            {
                Id = request.Id,
                Name = request.Name,
            });

            if (result)
            {
                var department = _departmentRepository.GetById(request.Id);
                return Task.FromResult(DepartmentDto.FromDepartment(department));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing department with id {request.Id}");
            }
        }
    }
}
