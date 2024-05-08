using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(int Id, int IllnessId,
       string PrescribedMedicine, int Duration) : IRequest<int>;

    public class AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler
        : IRequestHandler<AdjustTreatmentDetailsWithinDiagnosisMedicalRecord, int>
    {
        private readonly IRepository<Illness> _illnessRepository;
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;

        public AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler(IRepository<DiagnosisMedicalRecord> recordRepository, 
            IRepository<Illness> illnessRepository)
        {
            _recordRepository = recordRepository;
            _illnessRepository = illnessRepository;
        }

        public async Task<int> Handle(AdjustTreatmentDetailsWithinDiagnosisMedicalRecord request, CancellationToken cancellationToken)
        {
            var existingRecord = await _recordRepository.GetByIdAsync(request.Id);
            if (existingRecord == null)
            {
                throw new NoEntityFoundException($"Cannot update non-existing diagnosis medical record with id {request.Id}");
            }

            var illness = await _illnessRepository.GetByIdAsync(request.IllnessId);
            if (illness == null)
            {
                throw new NoEntityFoundException($"Cannot use non-existing illness to update diagnosis medical record with id {request.IllnessId}");
            }

            existingRecord.DiagnosedIllness = illness;
            existingRecord.ProposedTreatment.PrescribedMedicine = request.PrescribedMedicine;
            existingRecord.ProposedTreatment.DurationInDays = request.Duration;
            await _recordRepository.UpdateAsync(existingRecord);

            return await Task.FromResult(existingRecord.Id);
        }
    }
}
