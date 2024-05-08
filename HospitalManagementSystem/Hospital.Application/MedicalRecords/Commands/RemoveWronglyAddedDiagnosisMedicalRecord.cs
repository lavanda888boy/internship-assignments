using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record RemoveWronglyAddedDiagnosisMedicalRecord(int RecordId) : IRequest<int>;

    public class RemoveWronglyAddedDiagnosisMedicalRecordHandler :
        IRequestHandler<RemoveWronglyAddedDiagnosisMedicalRecord, int>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;

        public RemoveWronglyAddedDiagnosisMedicalRecordHandler(IRepository<DiagnosisMedicalRecord> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<int> Handle(RemoveWronglyAddedDiagnosisMedicalRecord request,
            CancellationToken cancellationToken)
        {
            var recordToDelete = await _recordRepository.GetByIdAsync(request.RecordId);

            if (recordToDelete == null)
            {
                throw new NoEntityFoundException($"There is no diagnosis medical record with id = {request.RecordId} to delete");
            }

            await _recordRepository.DeleteAsync(recordToDelete);
            return await Task.FromResult(recordToDelete.Id);
        }
    }
}
