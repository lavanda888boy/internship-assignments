using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Queries
{
    public record GetPatientsByProperty(Func<Patient, bool> PatientProperty) : IRequest<List<PatientDto>>;

    public class GetPatientsByPropertyHandler : IRequestHandler<GetPatientsByProperty, List<PatientDto>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientsByPropertyHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Task<List<PatientDto>> Handle(GetPatientsByProperty request, CancellationToken cancellationToken)
        {
            var patients = _patientRepository.SearchByProperty(request.PatientProperty);

            if (patients is null)
            {
                throw new NoEntityFoundException("No patients with such properties exist");
            }

            return Task.FromResult(patients.Select(PatientDto.FromPatient).ToList());
        }
    }
}
