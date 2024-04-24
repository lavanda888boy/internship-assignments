using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.Doctors.Queries
{
    public record SearchDoctorsByASetOfProperties(DoctorFilterDto DoctorFilters) : IRequest<List<DoctorDto>>;

    public class SearchDoctorsByASetOfPropertiesHandler : IRequestHandler<SearchDoctorsByASetOfProperties, List<DoctorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchDoctorsByASetOfPropertiesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DoctorDto>> Handle(SearchDoctorsByASetOfProperties request, CancellationToken cancellationToken)
        {
            Expression<Func<Doctor, bool>> predicate = d =>
                (string.IsNullOrEmpty(request.DoctorFilters.Name) || d.Name == request.DoctorFilters.Name) &&
                (string.IsNullOrEmpty(request.DoctorFilters.Surname) || d.Surname == request.DoctorFilters.Surname) &&
                (string.IsNullOrEmpty(request.DoctorFilters.Address) || d.Address == request.DoctorFilters.Address) &&
                (string.IsNullOrEmpty(request.DoctorFilters.PhoneNumber) || d.PhoneNumber == request.DoctorFilters.PhoneNumber) &&
                (string.IsNullOrEmpty(request.DoctorFilters.DepartmentName) || d.Department.Name == request.DoctorFilters.DepartmentName);

            var doctors = await _unitOfWork.DoctorRepository.SearchByPropertyAsync(predicate);

            if (doctors.Count == 0)
            {
                throw new NoEntityFoundException("No doctors with such properties exist");
            }

            return await Task.FromResult(doctors.Select(DoctorDto.FromDoctor).ToList());
        }
    }
}
