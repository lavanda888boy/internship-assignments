using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record UpdatePatientPersonalInfo(int Id, string Name, string Surname, int Age, string Gender,
        string Address, string? PhoneNumber, string? InsuranceNumber) : IRequest<int>;

    public class UpdatePatientPersonalInfoHandler
        : IRequestHandler<UpdatePatientPersonalInfo, int>
    {
        private readonly IRepository<Patient> _patientRepository;

        public UpdatePatientPersonalInfoHandler(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<int> Handle(UpdatePatientPersonalInfo request, CancellationToken cancellationToken)
        {
            var existingPatient = await _patientRepository.GetByIdAsync(request.Id);

            if (existingPatient != null)
            {
                existingPatient.Name = request.Name;
                existingPatient.Surname = request.Surname;
                existingPatient.Age = request.Age;
                existingPatient.Gender = Enum.Parse<Gender>(request.Gender);
                existingPatient.Address = request.Address;
                existingPatient.PhoneNumber = request.PhoneNumber;
                existingPatient.InsuranceNumber = request.InsuranceNumber;

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
