using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record UpdateDoctorAssignedPatients(int Id, List<int> AssignedPatientIds) : IRequest<DoctorDto>;

    public class UpdateDoctorAssignedPatientsHandler : IRequestHandler<UpdateDoctorAssignedPatients, DoctorDto>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public UpdateDoctorAssignedPatientsHandler(IDoctorRepository doctorRepository,
            IPatientRepository patientRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public Task<DoctorDto> Handle(UpdateDoctorAssignedPatients request, CancellationToken cancellationToken)
        {
            var existingDoctor = _doctorRepository.GetById(request.Id);
            if (existingDoctor != null)
            {
                var doctorPatientsIds = existingDoctor.AssignedPatients
                                                      .Select(p => p.Id).ToList();
                var patientsToAdd = request.AssignedPatientIds.Except(doctorPatientsIds).Select(_patientRepository.GetById).ToList();
                var patientsToRemove = doctorPatientsIds.Except(request.AssignedPatientIds).Select(_patientRepository.GetById).ToList();

                foreach (var patient in patientsToAdd)
                {
                    if (existingDoctor.TryAddPatient(patient))
                    {
                        patient.AddDoctor(existingDoctor);
                        _patientRepository.Update(patient);
                    }
                    else
                    {
                        throw new DoctorPatientAssignationException("Too many patients to add to the existing doctor");
                    }
                }

                foreach (var patient in patientsToRemove)
                {
                    existingDoctor.RemovePatient(patient.Id);
                    patient.RemoveDoctor(existingDoctor.Id);
                    _patientRepository.Update(patient);
                }

                _doctorRepository.Update(existingDoctor);
                return Task.FromResult(DoctorDto.FromDoctor(existingDoctor));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing doctor with id {request.Id}");
            }
        }
    }
}
