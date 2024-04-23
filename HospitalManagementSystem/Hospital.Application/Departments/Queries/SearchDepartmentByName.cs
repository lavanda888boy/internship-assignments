using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Application.Exceptions;
using MediatR;

namespace Hospital.Application.Departments.Queries
{
    public record SearchDepartmentByName(string DepartmentName) : IRequest<DepartmentDto>;

    public class SearchDepartmentByNameHandler : IRequestHandler<SearchDepartmentByName, DepartmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchDepartmentByNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDto> Handle(SearchDepartmentByName request, CancellationToken cancellationToken)
        {
            var departments = await _unitOfWork.DepartmentRepository.SearchByPropertyAsync(d => d.Name == request.DepartmentName);

            if (departments.Count == 0)
            {
                throw new NoEntityFoundException("No department with such name exists");
            }

            return await Task.FromResult(DepartmentDto.FromDepartment(departments[0]));
        }
    }
}
