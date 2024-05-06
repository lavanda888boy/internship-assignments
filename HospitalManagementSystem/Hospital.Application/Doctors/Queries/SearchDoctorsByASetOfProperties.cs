using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.Doctors.Queries
{
    public record SearchDoctorsByASetOfProperties(string? Name, string? Surname, string? Address,
        string? PhoneNumber, string? DepartmentName) : IRequest<List<DoctorDto>>;

    public class SearchDoctorsByASetOfPropertiesHandler : IRequestHandler<SearchDoctorsByASetOfProperties, List<DoctorDto>>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public SearchDoctorsByASetOfPropertiesHandler(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<List<DoctorDto>> Handle(SearchDoctorsByASetOfProperties request, CancellationToken cancellationToken)
        {
            Expression<Func<Doctor, bool>> predicate = d =>
                (string.IsNullOrEmpty(request.Name) || d.Name == request.Name) &&
                (string.IsNullOrEmpty(request.Surname) || d.Surname == request.Surname) &&
                (string.IsNullOrEmpty(request.Address) || d.Address == request.Address) &&
                (string.IsNullOrEmpty(request.PhoneNumber) || d.PhoneNumber == request.PhoneNumber) &&
                (string.IsNullOrEmpty(request.DepartmentName) || d.Department.Name == request.DepartmentName);

            var doctors = await _doctorRepository.SearchByPropertyAsync(predicate);

            if (doctors.Count == 0)
            {
                throw new NoEntityFoundException("No doctors with such properties exist");
            }

            var doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);
            return await Task.FromResult(doctorDtos);
        }
    }
}
