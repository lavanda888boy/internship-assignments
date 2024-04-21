//using Hospital.Application.Abstractions;
//using Hospital.Application.Exceptions;
//using Hospital.Application.MedicalRecords.Responses;
//using Hospital.Domain.Models;
//using MediatR;

//namespace Hospital.Application.MedicalRecords.Commands
//{
//    public record AddNewDiagnosisMedicalRecord(int Id, int PatientId, int DoctorId, string ExaminationNotes,
//       int IllnessId, int TreatmentId, string PrescribedMedicine, TimeSpan TreatmentDuration) 
//       : IRequest<DiagnosisMedicalRecordDto>;

//    public class AddNewDiagnosisMedicalRecordHandler
//        : IRequestHandler<AddNewDiagnosisMedicalRecord, DiagnosisMedicalRecordDto>
//    {
//        private readonly IDiagnosisMedicalRecordRepository _medicalRecordRepository;
//        private readonly IPatientRepository _patientRepository;
//        private readonly IDoctorRepository _doctorRepository;
//        private readonly IIllnessRepository _illnessRepository;
//        private readonly ITreatmentRepository _treatmentRepository;

//        public AddNewDiagnosisMedicalRecordHandler(IDiagnosisMedicalRecordRepository medicalRecordRepository,
//            IPatientRepository patientRepository, IDoctorRepository doctorRepository, 
//            IIllnessRepository illnessRepository, ITreatmentRepository treatmentRepository)
//        {
//            _medicalRecordRepository = medicalRecordRepository;
//            _patientRepository = patientRepository;
//            _doctorRepository = doctorRepository;
//            _illnessRepository = illnessRepository;
//            _treatmentRepository = treatmentRepository;
//        }

//        public Task<DiagnosisMedicalRecordDto> Handle(AddNewDiagnosisMedicalRecord request,
//            CancellationToken cancellationToken)
//        {
//            var examinedPatient = _patientRepository.GetById(request.PatientId);
//            var responsibleDoctor = _doctorRepository.GetById(request.DoctorId);
//            var illness = _illnessRepository.GetById(request.IllnessId);

//            if (examinedPatient == null)
//            {
//                throw new NoEntityFoundException($"Cannot create diagnosis medical record of a non-existing patient with id {request.PatientId}");
//            }

//            if (responsibleDoctor == null)
//            {
//                throw new NoEntityFoundException($"Cannot create diagnosis medical record for a non-existing doctor with id {request.DoctorId}");
//            }

//            if (illness == null)
//            {
//                throw new NoEntityFoundException($"Cannot create diagnosis medical record with a non-existing illness with id {request.IllnessId}");
//            }

//            bool examinedPatientIsAssignedToTheDoctor = examinedPatient.AssignedDoctors
//                                                                       .Any(d => d.Id == responsibleDoctor.Id);
//            if (examinedPatientIsAssignedToTheDoctor)
//            {
//                var medicalRecord = new DiagnosisMedicalRecord
//                {
//                    Id = request.Id,
//                    ExaminedPatient = examinedPatient,
//                    ResponsibleDoctor = responsibleDoctor,
//                    DateOfExamination = DateTimeOffset.UtcNow,
//                    ExaminationNotes = request.ExaminationNotes,
//                    DiagnosedIllness = illness,
//                    ProposedTreatment = new Treatment()
//                    {
//                        Id = request.TreatmentId,
//                        PrescribedMedicine = request.PrescribedMedicine,
//                        TreatmentDuration = request.TreatmentDuration,
//                    }
//                };

//                _treatmentRepository.Create(medicalRecord.ProposedTreatment);

//                var createdRecord = _medicalRecordRepository.Create(medicalRecord);
//                return Task.FromResult(DiagnosisMedicalRecordDto.FromMedicalRecord(createdRecord));
//            }
//            else
//            {
//                throw new PatientDoctorMisassignationException("Cannot create diagnosis medical record beacuse the patient is not assigned to the doctor");
//            }
//        }
//    }
//}
