using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Commands
{
    public record CreateDepartment(int Id, string Name) : IRequest<DepartmentDto>;

    public class CreateDepartmentHandler : IRequestHandler<CreateDepartment, DepartmentDto>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public CreateDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<DepartmentDto> Handle(CreateDepartment request, CancellationToken cancellationToken)
        {
            var department = _departmentRepository.Create(new Department()
            {
                Id = request.Id,
                Name = request.Name,
            });

            return Task.FromResult(DepartmentDto.FromDepartment(department));
        }
    }
}
