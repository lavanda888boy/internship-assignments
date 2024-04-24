using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AdjustDiagnosisMedicalRecordExaminationNotes(int Id, string ExaminationNotes)
        : IRequest<DiagnosisMedicalRecordDto>;

    public class AdjustDiagnosisMedicalRecordExaminationNotesHandler
        : IRequestHandler<AdjustDiagnosisMedicalRecordExaminationNotes, DiagnosisMedicalRecordDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdjustDiagnosisMedicalRecordExaminationNotesHandler(IUnitOfWork unitOFWork)
        {
            _unitOfWork = unitOFWork;
        }

        public async Task<DiagnosisMedicalRecordDto> Handle(AdjustDiagnosisMedicalRecordExaminationNotes request, CancellationToken cancellationToken)
        {
            var existingRecord = await _unitOfWork.DiagnosisRecordRepository.GetByIdAsync(request.Id);
            if (existingRecord == null)
            {
                throw new NoEntityFoundException($"Cannot update non-existing diagnosis medical record with id {request.Id}");
            }

            try
            {
                existingRecord.ExaminationNotes = request.ExaminationNotes;

                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.DiagnosisRecordRepository.UpdateAsync(existingRecord);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                await _unitOfWork.DiagnosisRecordRepository.UpdateAsync(existingRecord);
                return await Task.FromResult(DiagnosisMedicalRecordDto.FromMedicalRecord(existingRecord));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
