using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record DeletePatient(int PatientId) : IRequest<int>;

    public class DeletePatientHandler : IRequestHandler<DeletePatient, int>
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;

        public DeletePatientHandler(IRepository<Patient> patientRepository,
            IRepository<Doctor> doctorRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public Task<int> Handle(DeletePatient request, CancellationToken cancellationToken)
        {
            var patientToDelete = _patientRepository.GetById(request.PatientId);
            if (patientToDelete != null)
            {
                _patientRepository.Delete(request.PatientId);
                foreach (var item in patientToDelete.AssignedDoctors)
                {
                    item.RemovePatient(request.PatientId);
                    _doctorRepository.Update(item);
                }

                return Task.FromResult(request.PatientId);
            }
            else
            {
                throw new NoEntityFoundException($"Cannot delete non-existing patient with id {request.PatientId}");
            }
        }
    }
}
