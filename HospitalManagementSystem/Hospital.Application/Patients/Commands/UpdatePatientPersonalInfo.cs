using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record UpdatePatientPersonalInfo(int Id, string Name, string Surname, int Age, string Gender,
        string Address, string? PhoneNumber, string? InsuranceNumber) : IRequest<PatientDto>;

    public class UpdatePatientPersonalInfoHandler
        : IRequestHandler<UpdatePatientPersonalInfo, PatientDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePatientPersonalInfoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PatientDto> Handle(UpdatePatientPersonalInfo request, CancellationToken cancellationToken)
        {
            var existingPatient = await _unitOfWork.PatientRepository.GetByIdAsync(request.Id);

            if (existingPatient != null)
            {
                existingPatient.Name = request.Name;
                existingPatient.Surname = request.Surname;
                existingPatient.Age = request.Age;
                existingPatient.Gender = request.Gender;
                existingPatient.Address = request.Address;
                existingPatient.PhoneNumber = request.PhoneNumber;
                existingPatient.InsuranceNumber = request.InsuranceNumber;

                await _unitOfWork.PatientRepository.UpdateAsync(existingPatient);

                return await Task.FromResult(PatientDto.FromPatient(existingPatient));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing patient with id {request.Id}");
            }
        }
    }
}
