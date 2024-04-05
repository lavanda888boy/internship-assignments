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

        public Task<PatientDto> Handle(GetPatientById request, CancellationToken cancellationToken)
        {
            var patient = _patientRepository.GetById(request.PatientId);

            if (patient is null)
            {
                throw new NoEntityFoundException($"There is no patient with id {request.PatientId}");
            }

            return Task.FromResult(PatientDto.FromPatient(patient));
        }
    }
}
