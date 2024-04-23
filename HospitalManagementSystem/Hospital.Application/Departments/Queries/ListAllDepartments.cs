using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using MediatR;
namespace Hospital.Application.Departments.Queries
{
    public record ListAllDepartments() : IRequest<List<DepartmentDto>>;

    public class ListAllDepartmentsHandler : IRequestHandler<ListAllDepartments, List<DepartmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllDepartmentsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DepartmentDto>> Handle(ListAllDepartments request, CancellationToken cancellationToken)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return await Task.FromResult(departments.Select(DepartmentDto.FromDepartment).ToList());
        }
    }
}
