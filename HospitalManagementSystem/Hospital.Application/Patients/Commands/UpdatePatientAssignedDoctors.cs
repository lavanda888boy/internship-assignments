using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record UpdatePatientAssignedDoctors(int Id, List<int> AssignedDoctorIds) 
        : IRequest<PatientDto>;

    public class UpdatePatientAssignedDoctorsHandler 
        : IRequestHandler<UpdatePatientAssignedDoctors, PatientDto>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public UpdatePatientAssignedDoctorsHandler(IPatientRepository patientRepository,
            IDoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public Task<PatientDto> Handle(UpdatePatientAssignedDoctors request, CancellationToken cancellationToken)
        {
            var existingPatient = _patientRepository.GetById(request.Id);

            if (existingPatient != null)
            {
                var updatedPatient = new Patient()
                {
                    Id = request.Id,
                    Name = existingPatient.Name,
                    Surname = existingPatient.Surname,
                    Age = existingPatient.Age,
                    Gender = existingPatient.Gender,
                    Address = existingPatient.Address,
                    PhoneNumber = existingPatient.PhoneNumber,
                    InsuranceNumber = existingPatient.InsuranceNumber,
                };

                updatedPatient.AssignedDoctors = existingPatient.AssignedDoctors;

                var patientDoctorsIds = existingPatient.AssignedDoctors
                                                       .Select(d => d.Id).ToList();
                var doctorsIdsToAdd = request.AssignedDoctorIds.Except(patientDoctorsIds);
                var doctorsIdsToRemove = patientDoctorsIds.Except(request.AssignedDoctorIds);

                foreach (var item in doctorsIdsToAdd)
                {
                    var doctor = _doctorRepository.GetById(item);
                    if (doctor.AddPatient(updatedPatient))
                    {
                        updatedPatient.AddDoctor(doctor);
                        _doctorRepository.Update(doctor);
                    }
                    else
                    {
                        throw new DoctorAssignedPatientsLimitExceeded("Too many patients to add to the existing doctor");
                    }
                }

                foreach (var item in doctorsIdsToRemove)
                {
                    var doctor = _doctorRepository.GetById(item);
                    updatedPatient.RemoveDoctor(item);
                    doctor.RemovePatient(updatedPatient.Id);
                    _doctorRepository.Update(doctor);
                }

                _patientRepository.Update(updatedPatient);
                return Task.FromResult(PatientDto.FromPatient(updatedPatient));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing patient with id {request.Id}");
            }
        }
    }
}
