using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Queries
{
    public record GetPatientById(int PatientId) : IRequest<PatientDto>;

    public class GetPatientByIdHandler : IRequestHandler<GetPatientById, PatientDto>
    {
        private readonly IRepository<Patient> _patientRepository;

        public GetPatientByIdHandler(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientDto> Handle(GetPatientById request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);

            if (patient == null)
            {
                throw new NoEntityFoundException($"Patient with id {request.PatientId} does not exist");
            }

            return await Task.FromResult(PatientDto.FromPatient(patient));
        }
    }
}
