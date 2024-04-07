using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record GetAllDiagnosisMedicalRecords() : IRequest<List<DiagnosisMedicalRecordDto>>;

    public class GetAllDiagnosisMedicalRecordsHandler 
        : IRequestHandler<GetAllDiagnosisMedicalRecords, List<DiagnosisMedicalRecordDto>>
    {
        private readonly IDiagnosisMedicalRecordRepository _medicalRecordRepository;

        public GetAllDiagnosisMedicalRecordsHandler(IDiagnosisMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<List<DiagnosisMedicalRecordDto>> Handle(GetAllDiagnosisMedicalRecords request,
            CancellationToken cancellationToken)
        {
            var medicalRecords = _medicalRecordRepository.GetAll();
            return Task.FromResult(medicalRecords.Select(DiagnosisMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
