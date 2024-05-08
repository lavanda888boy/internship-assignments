using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record RemoveWronglyAddedRegularMedicalRecord(int RecordId) : IRequest<int>;

    public class RemoveWronglyAddedRegularMedicalRecordHandler :
        IRequestHandler<RemoveWronglyAddedRegularMedicalRecord, int>
    {
        private readonly IRepository<RegularMedicalRecord> _recordRepository;

        public RemoveWronglyAddedRegularMedicalRecordHandler(IRepository<RegularMedicalRecord> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<int> Handle(RemoveWronglyAddedRegularMedicalRecord request,
            CancellationToken cancellationToken)
        {
            var recordToDelete = await _recordRepository.GetByIdAsync(request.RecordId);

            if (recordToDelete == null)
            {
                throw new NoEntityFoundException($"There is no regular medical record with id = {request.RecordId} to delete");
            }

            await _recordRepository.DeleteAsync(recordToDelete);
            return await Task.FromResult(recordToDelete.Id);
        }
    }
}
