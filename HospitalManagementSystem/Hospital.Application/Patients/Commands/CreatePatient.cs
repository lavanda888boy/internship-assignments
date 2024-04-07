using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record CreatePatient(int Id, string Name, string Surname, int Age, string Gender,
        string Address, string? PhoneNumber, string? InsuranceNumber) : IRequest<PatientDto>;

    public class CreatePatientCommandHandler : IRequestHandler<CreatePatient, PatientDto>
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;

        public CreatePatientCommandHandler(IRepository<Patient> patientRepository, 
            IRepository<Doctor> doctorRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public Task<PatientDto> Handle(CreatePatient request, CancellationToken cancellationToken)
        {
            var patient = new Patient
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                Age = request.Age,
                Gender = request.Gender,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                InsuranceNumber = request.InsuranceNumber
            };

            var doctors = _doctorRepository.GetAll();
            foreach (var doctor in doctors)
            {
                if (doctor.AddPatient(patient))
                {
                    patient.AddDoctor(doctor);
                    _patientRepository.Create(patient);

                    return Task.FromResult(PatientDto.FromPatient(patient));
                }
            }
            
            throw new NoEntityFoundException("There are no available doctors to be assigned to the patient");
        }
    }
}
