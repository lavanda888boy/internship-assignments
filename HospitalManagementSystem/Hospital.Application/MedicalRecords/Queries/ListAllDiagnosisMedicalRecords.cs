using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record ListAllDiagnosisMedicalRecords() : IRequest<List<DiagnosisMedicalRecordDto>>;

    public class ListAllDiagnosisMedicalRecordsHandler 
        : IRequestHandler<ListAllDiagnosisMedicalRecords, List<DiagnosisMedicalRecordDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllDiagnosisMedicalRecordsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DiagnosisMedicalRecordDto>> Handle(ListAllDiagnosisMedicalRecords request,
            CancellationToken cancellationToken)
        {
            var medicalRecords = await _unitOfWork.DiagnosisRecordRepository.GetAllAsync();
            return await Task.FromResult(medicalRecords.Select(DiagnosisMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
