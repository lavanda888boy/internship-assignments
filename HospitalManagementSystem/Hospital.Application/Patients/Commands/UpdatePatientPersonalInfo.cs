using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record UpdatePatientPersonalInfo(int Id, string Name, string Surname, int Age, Gender Gender,
        string Address, string? PhoneNumber, string? InsuranceNumber) : IRequest<PatientDto>;

    public class UpdatePatientPersonalInfoHandler
        : IRequestHandler<UpdatePatientPersonalInfo, PatientDto>
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IMapper _mapper;

        public UpdatePatientPersonalInfoHandler(IRepository<Patient> patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PatientDto> Handle(UpdatePatientPersonalInfo request, CancellationToken cancellationToken)
        {
            var existingPatient = await _patientRepository.GetByIdAsync(request.Id);

            if (existingPatient != null)
            {
                existingPatient.Name = request.Name;
                existingPatient.Surname = request.Surname;
                existingPatient.Age = request.Age;
                existingPatient.Gender = request.Gender;
                existingPatient.Address = request.Address;
                existingPatient.PhoneNumber = request.PhoneNumber;
                existingPatient.InsuranceNumber = request.InsuranceNumber;

                await _patientRepository.UpdateAsync(existingPatient);

                return await Task.FromResult(_mapper.Map<PatientDto>(existingPatient));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing patient with id {request.Id}");
            }
        }
    }
}
