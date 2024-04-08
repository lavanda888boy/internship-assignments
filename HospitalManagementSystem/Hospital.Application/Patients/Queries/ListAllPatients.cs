using Hospital.Application.Abstractions;
using Hospital.Application.Patients.Responses;
using MediatR;

namespace Hospital.Application.Patients.Queries
{
    public record ListAllPatients() : IRequest<List<PatientDto>>;

    public class ListAllPatientsHandler : IRequestHandler<ListAllPatients, List<PatientDto>>
    {
        private readonly IPatientRepository _patientRepository;

        public ListAllPatientsHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Task<List<PatientDto>> Handle(ListAllPatients request, CancellationToken cancellationToken)
        {
            var patients = _patientRepository.GetAll();
            return Task.FromResult(patients.Select(PatientDto.FromPatient).ToList());
        }
    }
}
