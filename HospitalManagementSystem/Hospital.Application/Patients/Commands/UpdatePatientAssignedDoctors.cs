using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record UpdatePatientAssignedDoctors(int Id, List<int> DoctorIds) : IRequest<int>;

    public class UpdatePatientAssignedDoctorsHandler : IRequestHandler<UpdatePatientAssignedDoctors, int>
    {
        private readonly IRepository<Patient> _patientRepository;

        public UpdatePatientAssignedDoctorsHandler(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<int> Handle(UpdatePatientAssignedDoctors request, CancellationToken cancellationToken)
        {
            var existingPatient = await _patientRepository.GetByIdAsync(request.Id);
            if (existingPatient != null)
            {
                existingPatient.DoctorsPatients.Clear();

                existingPatient.DoctorsPatients = request.DoctorIds.Select(doctorId => new DoctorsPatients()
                {
                    DoctorId = doctorId,
                    PatientId = existingPatient.Id,
                }).ToList();

                await _patientRepository.UpdateAsync(existingPatient);

                return await Task.FromResult(existingPatient.Id);
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing patient with id {request.Id}");
            }
        }
    }
}
