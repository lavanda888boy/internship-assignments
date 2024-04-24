using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AdjustRegularMedicalRecordExaminationNotes(int Id, string ExaminationNotes) 
        : IRequest<RegularMedicalRecordDto>;

    public class AdjustRegularMedicalRecordExaminationNotesHandler 
        : IRequestHandler<AdjustRegularMedicalRecordExaminationNotes, RegularMedicalRecordDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdjustRegularMedicalRecordExaminationNotesHandler(IUnitOfWork unitOFWork)
        {
            _unitOfWork = unitOFWork;
        }

        public async Task<RegularMedicalRecordDto> Handle(AdjustRegularMedicalRecordExaminationNotes request, CancellationToken cancellationToken)
        {
            var existingRecord = await _unitOfWork.RegularRecordRepository.GetByIdAsync(request.Id);
            if (existingRecord == null)
            {
                throw new NoEntityFoundException($"Cannot update non-existing regular medical record with id {request.Id}");
            }

            try
            {
                existingRecord.ExaminationNotes = request.ExaminationNotes;
                    
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.RegularRecordRepository.UpdateAsync(existingRecord);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return await Task.FromResult(RegularMedicalRecordDto.FromMedicalRecord(existingRecord));
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
