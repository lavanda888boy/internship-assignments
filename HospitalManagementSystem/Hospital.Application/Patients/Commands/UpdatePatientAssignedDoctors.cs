//using Hospital.Application.Abstractions;
//using Hospital.Application.Exceptions;
//using Hospital.Application.Patients.Responses;
//using MediatR;

//namespace Hospital.Application.Patients.Commands
//{
//    public record UpdatePatientAssignedDoctors(int Id, List<int> AssignedDoctorIds) 
//        : IRequest<PatientDto>;

//    public class UpdatePatientAssignedDoctorsHandler 
//        : IRequestHandler<UpdatePatientAssignedDoctors, PatientDto>
//    {
//        private readonly IPatientRepository _patientRepository;
//        private readonly IDoctorRepository _doctorRepository;

//        public UpdatePatientAssignedDoctorsHandler(IPatientRepository patientRepository,
//            IDoctorRepository doctorRepository)
//        {
//            _patientRepository = patientRepository;
//            _doctorRepository = doctorRepository;
//        }

//        public Task<PatientDto> Handle(UpdatePatientAssignedDoctors request, CancellationToken cancellationToken)
//        {
//            var existingPatient = _patientRepository.GetById(request.Id);

//            if (existingPatient != null)
//            {
//                var patientDoctorsIds = existingPatient.AssignedDoctor
//                                                       .Select(d => d.Id).ToList();
//                var doctorsToAdd = request.AssignedDoctorIds.Except(patientDoctorsIds).Select(_doctorRepository.GetById).ToList();
//                var doctorsToRemove = patientDoctorsIds.Except(request.AssignedDoctorIds).Select(_doctorRepository.GetById).ToList();

//                foreach (var doctor in doctorsToAdd)
//                {
//                    if (doctor.TryAddPatient(existingPatient))
//                    {
//                        existingPatient.AddDoctor(doctor);
//                        _doctorRepository.Update(doctor);
//                    }
//                    else
//                    {
//                        throw new DoctorPatientAssignationException("Too many patients to add to the existing doctor");
//                    }
//                }

//                foreach (var doctor in doctorsToRemove)
//                {
//                    existingPatient.RemoveDoctor(doctor.Id);
//                    doctor.RemovePatient(existingPatient.Id);
//                    _doctorRepository.Update(doctor);
//                }

//                _patientRepository.Update(existingPatient);
//                return Task.FromResult(PatientDto.FromPatient(existingPatient));
//            }
//            else
//            {
//                throw new NoEntityFoundException($"Cannot update non-existing patient with id {request.Id}");
//            }
//        }
//    }
//}
