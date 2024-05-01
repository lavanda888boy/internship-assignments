using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record RemoveWronglyAddedPatient(int PatientId) : IRequest<PatientDto>;

    public class RemoveWronglyAddedPatientHandler : IRequestHandler<RemoveWronglyAddedPatient, PatientDto>
    {
        private readonly IRepository<Patient> _patientRepository;

        public RemoveWronglyAddedPatientHandler(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientDto> Handle(RemoveWronglyAddedPatient request, CancellationToken cancellationToken)
        {
            var patientToDelete = await _patientRepository.GetByIdAsync(request.PatientId);

            if (patientToDelete == null)
            {
                throw new NoEntityFoundException($"There is no patient with id = {request.PatientId} to delete");
            }

            await _patientRepository.DeleteAsync(patientToDelete);
            return await Task.FromResult(PatientDto.FromPatient(patientToDelete));
        }
    }
}
