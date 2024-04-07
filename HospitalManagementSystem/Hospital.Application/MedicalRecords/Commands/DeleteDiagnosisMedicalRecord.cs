using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record DeleteDiagnosisMedicalRecord(int RecordId) : IRequest<int>;

    public class DeleteDiagnosisMedicalRecordHandler
        : IRequestHandler<DeleteDiagnosisMedicalRecord, int>
    {
        private readonly IDiagnosisMedicalRecordRepository _medicalRecordRepository;

        public DeleteDiagnosisMedicalRecordHandler(IDiagnosisMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<int> Handle(DeleteDiagnosisMedicalRecord request, CancellationToken cancellationToken)
        {
            var result = _medicalRecordRepository.Delete(request.RecordId);

            if (result)
            {

                return Task.FromResult(request.RecordId);
            }
            else
            {
                throw new NoEntityFoundException($"Cannot delete non-existing diagnosis medical record with id {request.RecordId}");
            }
        }
    }
}
