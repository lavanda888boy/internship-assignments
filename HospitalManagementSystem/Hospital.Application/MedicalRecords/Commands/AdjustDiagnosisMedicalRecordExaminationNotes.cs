using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AdjustDiagnosisMedicalRecordExaminationNotes(int Id, string ExaminationNotes)
        : IRequest<int>;

    public class AdjustDiagnosisMedicalRecordExaminationNotesHandler
        : IRequestHandler<AdjustDiagnosisMedicalRecordExaminationNotes, int>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;

        public AdjustDiagnosisMedicalRecordExaminationNotesHandler(IRepository<DiagnosisMedicalRecord> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<int> Handle(AdjustDiagnosisMedicalRecordExaminationNotes request, CancellationToken cancellationToken)
        {
            var existingRecord = await _recordRepository.GetByIdAsync(request.Id);
            if (existingRecord == null)
            {
                throw new NoEntityFoundException($"Cannot update non-existing diagnosis medical record with id {request.Id}");
            }

            existingRecord.ExaminationNotes = request.ExaminationNotes;
            await _recordRepository.UpdateAsync(existingRecord);

            return await Task.FromResult(existingRecord.Id);
        }
    }
}
