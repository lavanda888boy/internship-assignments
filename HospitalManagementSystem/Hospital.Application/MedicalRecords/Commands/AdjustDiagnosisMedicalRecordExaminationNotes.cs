//using Hospital.Application.Abstractions;
//using Hospital.Application.Exceptions;
//using Hospital.Application.MedicalRecords.Responses;
//using Hospital.Domain.Models;
//using MediatR;

//namespace Hospital.Application.MedicalRecords.Commands
//{
//    public record AdjustDiagnosisMedicalRecordExaminationNotes(int Id, string ExaminationNotes) 
//        : IRequest<DiagnosisMedicalRecordDto>;

//    public class AdjustDiagnosisMedicalRecordExaminationNotesHandler
//        : IRequestHandler<AdjustDiagnosisMedicalRecordExaminationNotes, DiagnosisMedicalRecordDto>
//    {
//        private readonly IDiagnosisMedicalRecordRepository _medicalRecordRepository;

//        public AdjustDiagnosisMedicalRecordExaminationNotesHandler(IDiagnosisMedicalRecordRepository medicalRecordRepository)
//        {
//            _medicalRecordRepository = medicalRecordRepository;
//        }

//        public Task<DiagnosisMedicalRecordDto> Handle(AdjustDiagnosisMedicalRecordExaminationNotes request, CancellationToken cancellationToken)
//        {
//            var existingRecord = _medicalRecordRepository.GetById(request.Id);
//            if (existingRecord == null)
//            {
//                throw new NoEntityFoundException($"Cannot update non-existing diagnosis medical record with id {request.Id}");
//            }
//            else
//            {
//                var updatedRecord = new DiagnosisMedicalRecord()
//                {
//                    Id = request.Id,
//                    ExaminedPatient = existingRecord.ExaminedPatient,
//                    ResponsibleDoctor = existingRecord.ResponsibleDoctor,
//                    DateOfExamination = existingRecord.DateOfExamination,
//                    ExaminationNotes = request.ExaminationNotes,
//                    DiagnosedIllness = existingRecord.DiagnosedIllness,
//                    ProposedTreatment = existingRecord.ProposedTreatment,
//                };

//                _medicalRecordRepository.Update(updatedRecord);
//                return Task.FromResult(DiagnosisMedicalRecordDto.FromMedicalRecord(updatedRecord));
//            }
//        }
//    }
//}
