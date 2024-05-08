using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record RemoveWronglyAddedPatient(int PatientId) : IRequest<int>;

    public class RemoveWronglyAddedPatientHandler : IRequestHandler<RemoveWronglyAddedPatient, int>
    {
        private readonly IRepository<Patient> _patientRepository;

        public RemoveWronglyAddedPatientHandler(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<int> Handle(RemoveWronglyAddedPatient request, CancellationToken cancellationToken)
        {
            var patientToDelete = await _patientRepository.GetByIdAsync(request.PatientId);

            if (patientToDelete == null)
            {
                throw new NoEntityFoundException($"There is no patient with id = {request.PatientId} to delete");
            }

            await _patientRepository.DeleteAsync(patientToDelete);
            return await Task.FromResult(patientToDelete.Id);
        }
    }
}
