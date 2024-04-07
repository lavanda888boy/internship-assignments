using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record DeleteRegularMedicalRecord(int RecordId) : IRequest<int>;

    public class DeleteRegularMedicalRecordHandler 
        : IRequestHandler<DeleteRegularMedicalRecord, int>
    {
        private readonly IRegularMedicalRecordRepository _medicalRecordRepository;

        public DeleteRegularMedicalRecordHandler(IRegularMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<int> Handle(DeleteRegularMedicalRecord request, CancellationToken cancellationToken)
        {
            var result = _medicalRecordRepository.Delete(request.RecordId);

            if (result)
            {
                return Task.FromResult(request.RecordId);
            }
            else
            {
                throw new NoEntityFoundException($"Cannot delete non-existing regular medical record with id {request.RecordId}");
            }
        }
    }
}
