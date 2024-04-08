using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
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
                var updatedDoctor = new Doctor()
                {
                    Id = request.Id,
                    Name = existingDoctor.Name,
                    Surname = existingDoctor.Surname,
                    Address = existingDoctor.Address,
                    PhoneNumber = existingDoctor.PhoneNumber,
                    Department = existingDoctor.Department,
                    WorkingHours = existingDoctor.WorkingHours,
                };

                updatedDoctor.AssignedPatients = existingDoctor.AssignedPatients;

                var doctorPatientsIds = existingDoctor.AssignedPatients
                                                      .Select(p => p.Id).ToList();
                var patientsIdsToAdd = request.AssignedPatientIds.Except(doctorPatientsIds);
                var patientsIdsToRemove = doctorPatientsIds.Except(request.AssignedPatientIds);

                foreach (var item in patientsIdsToAdd)
                {
                    var patient = _patientRepository.GetById(item);
                    if (updatedDoctor.AddPatient(patient))
                    {
                        patient.AddDoctor(updatedDoctor);
                        _patientRepository.Update(patient);
                    }
                    else
                    {
                        throw new DoctorAssignedPatientsLimitExceeded("Too many patients to add to the existing doctor");
                    }
                }

                foreach (var item in patientsIdsToRemove)
                {
                    var patient = _patientRepository.GetById(item);
                    updatedDoctor.RemovePatient(item);
                    patient.RemoveDoctor(updatedDoctor.Id);
                    _patientRepository.Update(patient);
                }

                _doctorRepository.Update(updatedDoctor);
                return Task.FromResult(DoctorDto.FromDoctor(updatedDoctor));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing doctor with id {request.Id}");
            }
        }
    }
}
