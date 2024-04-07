using Hospital.Application.Abstractions;
using Hospital.Application.Patients.Responses;
using MediatR;

namespace Hospital.Application.Patients.Queries
{
    public record GetAllPatients() : IRequest<List<PatientDto>>;

    public class GetAllPatientsHandler : IRequestHandler<GetAllPatients, List<PatientDto>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetAllPatientsHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Task<List<PatientDto>> Handle(GetAllPatients request, CancellationToken cancellationToken)
        {
            var patients = _patientRepository.GetAll();
            return Task.FromResult(patients.Select(PatientDto.FromPatient).ToList());
        }
    }
}
