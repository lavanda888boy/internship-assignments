using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(int Id, int IllnessId,
       string PrescribedMedicine, TimeSpan TreatmentDuration) : IRequest<DiagnosisMedicalRecordDto>;

    public class AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler 
        : IRequestHandler<AdjustTreatmentDetailsWithinDiagnosisMedicalRecord, DiagnosisMedicalRecordDto>
    {
        private readonly IDiagnosisMedicalRecordRepository _medicalRecordRepository;
        private readonly IIllnessRepository _illnessRepository;
        private readonly ITreatmentRepository _treatmentRepository;

        public AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler(IDiagnosisMedicalRecordRepository medicalRecordRepository,
            IIllnessRepository illnessRepository, ITreatmentRepository treatmentRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _illnessRepository = illnessRepository;
            _treatmentRepository = treatmentRepository;
        }

        public Task<DiagnosisMedicalRecordDto> Handle(AdjustTreatmentDetailsWithinDiagnosisMedicalRecord request, CancellationToken cancellationToken)
        {
            var existingRecord = _medicalRecordRepository.GetById(request.Id);
            if (existingRecord == null)
            {
                throw new NoEntityFoundException($"Cannot update non-existing diagnosis medical record with id {request.Id}");
            }
            else
            {
                existingRecord.DiagnosedIllness = _illnessRepository.GetById(request.IllnessId);
                existingRecord.ProposedTreatment.PrescribedMedicine = request.PrescribedMedicine;
                existingRecord.ProposedTreatment.TreatmentDuration = request.TreatmentDuration;

                _treatmentRepository.Update(existingRecord.ProposedTreatment);
                _medicalRecordRepository.Update(existingRecord);

                return Task.FromResult(DiagnosisMedicalRecordDto.FromMedicalRecord(existingRecord));
            }
        }
    }
}
