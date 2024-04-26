using Hospital.Application.Abstractions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Queries
{
    public record ListAllPatients() : IRequest<List<PatientDto>>;

    public class ListAllPatientsHandler : IRequestHandler<ListAllPatients, List<PatientDto>>
    {
        private readonly IRepository<Patient> _patientRepository;

        public ListAllPatientsHandler(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<PatientDto>> Handle(ListAllPatients request, CancellationToken cancellationToken)
        {
            var patients = await _patientRepository.GetAllAsync();
            return await Task.FromResult(patients.Select(PatientDto.FromPatient).ToList());
        }
    }
}
