using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(int Id, int IllnessId,
       string PrescribedMedicine, int Duration) : IRequest<DiagnosisMedicalRecordDto>;

    public class AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler
        : IRequestHandler<AdjustTreatmentDetailsWithinDiagnosisMedicalRecord, DiagnosisMedicalRecordDto>
    {
        private readonly IRepository<Illness> _illnessRepository;
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler(IRepository<DiagnosisMedicalRecord> recordRepository, 
            IRepository<Illness> illnessRepository, IMapper mapper)
        {
            _recordRepository = recordRepository;
            _illnessRepository = illnessRepository;
            _mapper = mapper;
        }

        public async Task<DiagnosisMedicalRecordDto> Handle(AdjustTreatmentDetailsWithinDiagnosisMedicalRecord request, CancellationToken cancellationToken)
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

            return await Task.FromResult(_mapper.Map<DiagnosisMedicalRecordDto>(existingRecord));
        }
    }
}
