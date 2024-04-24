using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Commands
{
    public record ChangeDepartmentClassification(int Id, string Name) : IRequest<DepartmentDto>;

    public class ChangeDepartmentClassificationHandler : IRequestHandler<ChangeDepartmentClassification, DepartmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeDepartmentClassificationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDto> Handle(ChangeDepartmentClassification request, CancellationToken cancellationToken)
        {
            var department = new Department()
            {
                Id = request.Id,
                Name = request.Name,
            };

            await _unitOfWork.DepartmentRepository.UpdateAsync(department);

            return await Task.FromResult(DepartmentDto.FromDepartment(department));
        }
    }
}
