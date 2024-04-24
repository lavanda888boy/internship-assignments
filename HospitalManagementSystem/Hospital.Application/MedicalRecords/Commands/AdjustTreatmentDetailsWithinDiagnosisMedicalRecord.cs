using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(int Id, int IllnessId,
       string PrescribedMedicine, TimeSpan TreatmentDuration) : IRequest<DiagnosisMedicalRecordDto>;

    public class AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler
        : IRequestHandler<AdjustTreatmentDetailsWithinDiagnosisMedicalRecord, DiagnosisMedicalRecordDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler(IUnitOfWork unitOFWork)
        {
            _unitOfWork = unitOFWork;
        }

        public async Task<DiagnosisMedicalRecordDto> Handle(AdjustTreatmentDetailsWithinDiagnosisMedicalRecord request, CancellationToken cancellationToken)
        {
            var existingRecord = await _unitOfWork.DiagnosisRecordRepository.GetByIdAsync(request.Id);
            if (existingRecord == null)
            {
                throw new NoEntityFoundException($"Cannot update non-existing diagnosis medical record with id {request.Id}");
            }

            var illness = await _unitOfWork.IllnessRepository.GetByIdAsync(request.IllnessId);
            if (illness == null)
            {
                throw new NoEntityFoundException($"Cannot use non-existing illness to update diagnosis medical record with id {request.IllnessId}");
            }

            existingRecord.DiagnosedIllness = illness;
            existingRecord.ProposedTreatment.PrescribedMedicine = request.PrescribedMedicine;
            existingRecord.ProposedTreatment.Duration = request.TreatmentDuration;
            await _unitOfWork.DiagnosisRecordRepository.UpdateAsync(existingRecord);

            return await Task.FromResult(DiagnosisMedicalRecordDto.FromMedicalRecord(existingRecord));
        }
    }
}
