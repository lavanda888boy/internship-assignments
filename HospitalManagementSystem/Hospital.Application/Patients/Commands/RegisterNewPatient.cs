using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record RegisterNewPatient(string Name, string Surname, int Age, string Gender,
        string Address, string? PhoneNumber, string? InsuranceNumber) : IRequest<PatientDto>;

    public class RegisterNewPatientHandler : IRequestHandler<RegisterNewPatient, PatientDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterNewPatientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PatientDto> Handle(RegisterNewPatient request, CancellationToken cancellationToken)
        {
            var patient = new Patient
            {
                Name = request.Name,
                Surname = request.Surname,
                Age = request.Age,
                Gender = request.Gender,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                InsuranceNumber = request.InsuranceNumber
            };

            var doctors = await _unitOfWork.DoctorRepository.GetAllAsync();

            if (doctors.Count == 0)
            {
                throw new NoEntityFoundException("There are no available doctors to be assigned to the patient");
            }

            Random r = new Random();
            int doctorId = r.Next(1, doctors.Count);

            patient.DoctorsPatients = new List<DoctorsPatients>()
            {
                new DoctorsPatients()
                {
                    DoctorId = doctorId,
                }
            };

            await _unitOfWork.PatientRepository.AddAsync(patient);
            return await Task.FromResult(PatientDto.FromPatient(patient));
        }
    }
}
