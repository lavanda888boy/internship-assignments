using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record ListAllDiagnosisMedicalRecords() : IRequest<List<DiagnosisMedicalRecordDto>>;

    public class ListAllDiagnosisMedicalRecordsHandler 
        : IRequestHandler<ListAllDiagnosisMedicalRecords, List<DiagnosisMedicalRecordDto>>
    {
        private readonly IDiagnosisMedicalRecordRepository _medicalRecordRepository;

        public ListAllDiagnosisMedicalRecordsHandler(IDiagnosisMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<List<DiagnosisMedicalRecordDto>> Handle(ListAllDiagnosisMedicalRecords request,
            CancellationToken cancellationToken)
        {
            var medicalRecords = _medicalRecordRepository.GetAll();
            return Task.FromResult(medicalRecords.Select(DiagnosisMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
