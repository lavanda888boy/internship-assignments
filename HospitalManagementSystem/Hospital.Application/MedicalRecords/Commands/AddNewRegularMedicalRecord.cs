//using Hospital.Application.Abstractions;
//using Hospital.Application.Exceptions;
//using Hospital.Application.MedicalRecords.Responses;
//using Hospital.Domain.Models;
//using MediatR;

//namespace Hospital.Application.MedicalRecords.Commands
//{
//    public record AddNewRegularMedicalRecord(int Id, int PatientId, int DoctorId, 
//        string ExaminationNotes) : IRequest<RegularMedicalRecordDto>;

//    public class AddNewRegularMedicalRecordHandler 
//        : IRequestHandler<AddNewRegularMedicalRecord, RegularMedicalRecordDto>
//    {
//        private readonly IRegularMedicalRecordRepository _medicalRecordRepository;
//        private readonly IPatientRepository _patientRepository;
//        private readonly IDoctorRepository _doctorRepository;

//        public AddNewRegularMedicalRecordHandler(IRegularMedicalRecordRepository medicalRecordRepository,
//            IPatientRepository patientRepository, IDoctorRepository doctorRepository)
//        {
//            _medicalRecordRepository = medicalRecordRepository;
//            _patientRepository = patientRepository;
//            _doctorRepository = doctorRepository;
//        }

//        public Task<RegularMedicalRecordDto> Handle(AddNewRegularMedicalRecord request,
//            CancellationToken cancellationToken)
//        {
//            var examinedPatient = _patientRepository.GetById(request.PatientId);
//            var responsibleDoctor = _doctorRepository.GetById(request.DoctorId);

//            if (examinedPatient == null)
//            {
//                throw new NoEntityFoundException($"Cannot create regular medical record of a non-existing patient with id {request.PatientId}");
//            }

//            if (responsibleDoctor == null)
//            {
//                throw new NoEntityFoundException($"Cannot create regular medical record for a non-existing doctor with id {request.DoctorId}");
//            }

//            bool examinedPatientIsAssignedToTheDoctor = examinedPatient.AssignedDoctor
//                                                                       .Any(d => d.Id == responsibleDoctor.Id);
//            if (examinedPatientIsAssignedToTheDoctor)
//            {
//                var medicalRecord = new RegularMedicalRecord
//                {
//                    Id = request.Id,
//                    ExaminedPatient = examinedPatient,
//                    ResponsibleDoctor = responsibleDoctor,
//                    DateOfExamination = DateTimeOffset.UtcNow,
//                    ExaminationNotes = request.ExaminationNotes
//                };

//                var createdRecord = _medicalRecordRepository.Create(medicalRecord);
//                return Task.FromResult(RegularMedicalRecordDto.FromMedicalRecord(createdRecord));
//            }
//            else
//            {
//                throw new PatientDoctorMisassignationException("Cannot create regular medical record beacuse the patient is not assigned to the doctor");
//            }
//        }
//    }
//}
