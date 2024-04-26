using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.Patients.Queries
{
    public record SearchPatientsByASetOfProperties(PatientFilterDto PatientFilters) : IRequest<List<PatientDto>>;

    public class SearchPatientsByASetOfPropertiesHandler : IRequestHandler<SearchPatientsByASetOfProperties, List<PatientDto>>
    {
        private readonly IRepository<Patient> _patientRepository;

        public SearchPatientsByASetOfPropertiesHandler(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<PatientDto>> Handle(SearchPatientsByASetOfProperties request, CancellationToken cancellationToken)
        {
            Expression<Func<Patient, bool>> predicate = p =>
                (string.IsNullOrEmpty(request.PatientFilters.Name) || p.Name == request.PatientFilters.Name) &&
                (string.IsNullOrEmpty(request.PatientFilters.Surname) || p.Surname == request.PatientFilters.Surname) &&
                (request.PatientFilters.Age == 0  || p.Age == request.PatientFilters.Age) &&
                (string.IsNullOrEmpty(request.PatientFilters.Gender) || p.Gender == request.PatientFilters.Gender) &&
                (string.IsNullOrEmpty(request.PatientFilters.Address) || p.Address == request.PatientFilters.Address) &&
                (string.IsNullOrEmpty(request.PatientFilters.PhoneNumber) || p.PhoneNumber == request.PatientFilters.PhoneNumber) &&
                (string.IsNullOrEmpty(request.PatientFilters.InsuranceNumber) || p.InsuranceNumber == request.PatientFilters.InsuranceNumber);

            var patients = await _patientRepository.SearchByPropertyAsync(predicate);

            if (patients.Count == 0)
            {
                throw new NoEntityFoundException("No patients with such properties exist");
            }

            return await Task.FromResult(patients.Select(PatientDto.FromPatient).ToList());
        }
    }
}
