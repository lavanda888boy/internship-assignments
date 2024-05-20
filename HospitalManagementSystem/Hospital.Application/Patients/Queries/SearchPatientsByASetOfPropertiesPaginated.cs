using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.Patients.Queries
{
    public record SearchPatientsByASetOfPropertiesPaginated(int PageNumber, int PageSize, string? Name, string? Surname, int? Age, string? Gender,
        string? Address, string? PhoneNumber, string? InsuranceNumber) : IRequest<PaginatedResult<PatientFullInfoDto>>;

    public class SearchPatientsByASetOfPropertiesPaginatedHandler 
        : IRequestHandler<SearchPatientsByASetOfPropertiesPaginated, PaginatedResult<PatientFullInfoDto>>
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IMapper _mapper;

        public SearchPatientsByASetOfPropertiesPaginatedHandler(IRepository<Patient> patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<PatientFullInfoDto>> Handle(SearchPatientsByASetOfPropertiesPaginated request, CancellationToken cancellationToken)
        {
            Expression<Func<Patient, bool>> predicate = p =>
                (string.IsNullOrEmpty(request.Name) || p.Name == request.Name) &&
                (string.IsNullOrEmpty(request.Surname) || p.Surname == request.Surname) &&
                (request.Age == 0 || p.Age == request.Age) &&
                (string.IsNullOrEmpty(request.Gender) || p.Gender == Enum.Parse<Gender>(request.Gender)) &&
                (string.IsNullOrEmpty(request.Address) || p.Address == request.Address) &&
                (string.IsNullOrEmpty(request.PhoneNumber) || p.PhoneNumber == request.PhoneNumber) &&
                (string.IsNullOrEmpty(request.InsuranceNumber) || p.InsuranceNumber == request.InsuranceNumber);

            var paginatedPatients = await _patientRepository.SearchByPropertyPaginatedAsync(predicate, 
                request.PageNumber, request.PageSize);

            if (paginatedPatients.Items.Count == 0)
            {
                throw new NoEntityFoundException("No patients with such properties exist");
            }

            return await Task.FromResult(new PaginatedResult<PatientFullInfoDto>
            {
                TotalItems = paginatedPatients.TotalItems,
                Items = _mapper.Map<List<PatientFullInfoDto>>(paginatedPatients.Items)
            });
        }
    }
}
