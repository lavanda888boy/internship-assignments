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
        private readonly IDepartmentRepository _departmentRepository;

        public ChangeDepartmentClassificationHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<DepartmentDto> Handle(ChangeDepartmentClassification request, CancellationToken cancellationToken)
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
