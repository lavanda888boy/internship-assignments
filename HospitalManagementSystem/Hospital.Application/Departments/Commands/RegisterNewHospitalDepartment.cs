using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Commands
{
    public record RegisterNewHospitalDepartment(string Name) : IRequest<DepartmentDto>;

    public class RegisterNewHospitalDepartmentHandler : IRequestHandler<RegisterNewHospitalDepartment, DepartmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterNewHospitalDepartmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDto> Handle(RegisterNewHospitalDepartment request, CancellationToken cancellationToken)
        {
            var department = new Department()
            {
                Name = request.Name,
            };

            var newDepartment = await _unitOfWork.DepartmentRepository.AddAsync(department);

            return await Task.FromResult(DepartmentDto.FromDepartment(newDepartment));
        }
    }
}
