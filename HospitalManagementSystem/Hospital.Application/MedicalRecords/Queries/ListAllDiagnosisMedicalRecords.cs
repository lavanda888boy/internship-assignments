using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record ListAllDiagnosisMedicalRecords() : IRequest<List<DiagnosisMedicalRecordDto>>;

    public class ListAllDiagnosisMedicalRecordsHandler 
        : IRequestHandler<ListAllDiagnosisMedicalRecords, List<DiagnosisMedicalRecordDto>>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;

        public ListAllDiagnosisMedicalRecordsHandler(IRepository<DiagnosisMedicalRecord> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<List<DiagnosisMedicalRecordDto>> Handle(ListAllDiagnosisMedicalRecords request,
            CancellationToken cancellationToken)
        {
            var medicalRecords = await _recordRepository.GetAllAsync();
            return await Task.FromResult(medicalRecords.Select(DiagnosisMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
