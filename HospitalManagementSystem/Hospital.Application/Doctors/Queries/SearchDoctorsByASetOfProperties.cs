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
            Expression<Func<Doctor, bool>> predicate = p =>
                (string.IsNullOrEmpty(request.DoctorFilters.Name) || p.Name == request.DoctorFilters.Name) &&
                (string.IsNullOrEmpty(request.DoctorFilters.Surname) || p.Surname == request.DoctorFilters.Surname) &&
                (string.IsNullOrEmpty(request.DoctorFilters.Address) || p.Address == request.DoctorFilters.Address) &&
                (string.IsNullOrEmpty(request.DoctorFilters.PhoneNumber) || p.PhoneNumber == request.DoctorFilters.PhoneNumber) &&
                (request.DoctorFilters.DepartmentId == 0 || p.Department.Id == request.DoctorFilters.DepartmentId);

            var doctors = _doctorRepository.SearchByProperty(predicate.Compile());

            if (doctors is null)
            {
                throw new NoEntityFoundException("No doctors with such properties exist");
            }

            return Task.FromResult(doctors.Select(DoctorDto.FromDoctor).ToList());
        }
    }
}
