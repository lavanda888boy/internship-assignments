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
        private readonly IDoctorRepository _doctorRepository;

        public SearchDoctorsByASetOfPropertiesHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Task<List<DoctorDto>> Handle(SearchDoctorsByASetOfProperties request, CancellationToken cancellationToken)
        {
            Expression<Func<Doctor, bool>> predicate = d =>
                (string.IsNullOrEmpty(request.DoctorFilters.Name) || d.Name == request.DoctorFilters.Name) &&
                (string.IsNullOrEmpty(request.DoctorFilters.Surname) || d.Surname == request.DoctorFilters.Surname) &&
                (string.IsNullOrEmpty(request.DoctorFilters.Address) || d.Address == request.DoctorFilters.Address) &&
                (string.IsNullOrEmpty(request.DoctorFilters.PhoneNumber) || d.PhoneNumber == request.DoctorFilters.PhoneNumber) &&
                (string.IsNullOrEmpty(request.DoctorFilters.DepartmentName) || d.Department.Name == request.DoctorFilters.DepartmentName);

            var doctors = _doctorRepository.SearchByProperty(predicate.Compile());

            if (doctors is null)
            {
                throw new NoEntityFoundException("No doctors with such properties exist");
            }

            return Task.FromResult(doctors.Select(DoctorDto.FromDoctor).ToList());
        }
    }
}
