//using Hospital.Application.Abstractions;
//using Hospital.Application.Exceptions;
//using Hospital.Application.Patients.Responses;
//using Hospital.Domain.Models;
//using MediatR;

//namespace Hospital.Application.Patients.Commands
//{
//    public record UpdatePatientPersonalInfo(int Id, string Name, string Surname, int Age, string Gender,
//        string Address, string? PhoneNumber, string? InsuranceNumber) : IRequest<PatientDto>;

//    public class UpdatePatientPersonalInfoHandler
//        : IRequestHandler<UpdatePatientPersonalInfo, PatientDto>
//    {
//        private readonly IPatientRepository _patientRepository;

//        public UpdatePatientPersonalInfoHandler(IPatientRepository patientRepository)
//        {
//            _patientRepository = patientRepository;
//        }

//        public Task<PatientDto> Handle(UpdatePatientPersonalInfo request, CancellationToken cancellationToken)
//        {
//            var existingPatient = _patientRepository.GetById(request.Id);

//            if (existingPatient != null)
//            {
//                var updatedPatient = new Patient()
//                {
//                    Id = request.Id,
//                    Name = request.Name,
//                    Surname = request.Surname,
//                    Age = request.Age,
//                    Gender = request.Gender,
//                    Address = request.Address,
//                    PhoneNumber = request.PhoneNumber,
//                    InsuranceNumber = request.InsuranceNumber,
//                };

//                updatedPatient.AssignedDoctor = existingPatient.AssignedDoctor;
//                _patientRepository.Update(updatedPatient);

//                return Task.FromResult(PatientDto.FromPatient(updatedPatient));
//            }
//            else
//            {
//                throw new NoEntityFoundException($"Cannot update non-existing patient with id {request.Id}");
//            }
//        }
//    }
//}
