using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Commands
{
    public record RegisterNewHospitalDepartment(string Name) : IRequest<DepartmentDto>;

    public class RegisterNewHospitalDepartmentHandler : IRequestHandler<RegisterNewHospitalDepartment, DepartmentDto>
    {
        private readonly IRepository<Department> _departmentRepository;

        public RegisterNewHospitalDepartmentHandler(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentDto> Handle(RegisterNewHospitalDepartment request, CancellationToken cancellationToken)
        {
            var department = new Department()
            {
                Name = request.Name,
            };

            var newDepartment = await _departmentRepository.AddAsync(department);

            return await Task.FromResult(DepartmentDto.FromDepartment(newDepartment));
        }
    }
}
