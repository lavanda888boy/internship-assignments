using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AdjustRegularMedicalRecordExaminationNotes(int Id, string ExaminationNotes) 
        : IRequest<RegularMedicalRecordDto>;

    public class AdjustRegularMedicalRecordExaminationNotesHandler 
        : IRequestHandler<AdjustRegularMedicalRecordExaminationNotes, RegularMedicalRecordDto>
    {
        private readonly IRepository<RegularMedicalRecord> _recordRepository;

        public AdjustRegularMedicalRecordExaminationNotesHandler(IRepository<RegularMedicalRecord> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<RegularMedicalRecordDto> Handle(AdjustRegularMedicalRecordExaminationNotes request, CancellationToken cancellationToken)
        {
            var existingRecord = await _recordRepository.GetByIdAsync(request.Id);
            if (existingRecord == null)
            {
                throw new NoEntityFoundException($"Cannot update non-existing regular medical record with id {request.Id}");
            }

            existingRecord.ExaminationNotes = request.ExaminationNotes;
            await _recordRepository.UpdateAsync(existingRecord);

            return await Task.FromResult(RegularMedicalRecordDto.FromMedicalRecord(existingRecord));
        }
    }
}
