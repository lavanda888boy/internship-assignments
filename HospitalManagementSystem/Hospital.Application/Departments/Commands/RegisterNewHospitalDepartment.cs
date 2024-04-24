using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Commands
{
    public record RegisterNewHospitalDepartment(int Id, string Name) : IRequest<DepartmentDto>;

    public class RegisterNewHospitalDepartmentHandler : IRequestHandler<RegisterNewHospitalDepartment, DepartmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterNewHospitalDepartmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDto> Handle(RegisterNewHospitalDepartment request, CancellationToken cancellationToken)
        {
            try
            {
                var department = new Department()
                {
                    Name = request.Name,
                };

                await _unitOfWork.BeginTransactionAsync();
                var newDepartment = _unitOfWork.DepartmentRepository.Add(department);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return await Task.FromResult(DepartmentDto.FromDepartment(department));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
