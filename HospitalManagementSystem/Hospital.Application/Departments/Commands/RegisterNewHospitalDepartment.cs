using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Commands
{
    public record RegisterNewHospitalDepartment(int Id, string Name) : IRequest<DepartmentDto>;

    public class RegisterNewHospitalDepartmentHandler : IRequestHandler<RegisterNewHospitalDepartment, DepartmentDto>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public RegisterNewHospitalDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<DepartmentDto> Handle(RegisterNewHospitalDepartment request, CancellationToken cancellationToken)
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
