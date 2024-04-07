using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record UpdatePatient(int Id, string Name, string Surname, int Age, string Gender,
        string Address, string? PhoneNumber, string? InsuranceNumber, List<int> AssignedDoctorIds) 
        : IRequest<PatientDto>;

    public class UpdatePatientHandler : IRequestHandler<UpdatePatient, PatientDto>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public UpdatePatientHandler(IPatientRepository patientRepository,
            IDoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public Task<PatientDto> Handle(UpdatePatient request, CancellationToken cancellationToken)
        {
            var updatedPatient = new Patient()
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                Age = request.Age,
                Gender = request.Gender,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                InsuranceNumber = request.InsuranceNumber,
            };

            var existingPatient = _patientRepository.GetById(request.Id);
            if (existingPatient != null)
            {
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
