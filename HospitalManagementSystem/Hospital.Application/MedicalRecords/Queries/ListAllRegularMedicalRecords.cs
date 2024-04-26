using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record ListAllRegularMedicalRecords()
        : IRequest<List<RegularMedicalRecordDto>>;

    public class ListAllRegularMedicalRecordsHandler
        : IRequestHandler<ListAllRegularMedicalRecords, List<RegularMedicalRecordDto>>
    {
        private readonly IRepository<RegularMedicalRecord> _recordRepository;

        public ListAllRegularMedicalRecordsHandler(IRepository<RegularMedicalRecord> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<List<RegularMedicalRecordDto>> Handle(ListAllRegularMedicalRecords request, CancellationToken cancellationToken)
        {
            var medicalRecords = await _recordRepository.GetAllAsync();
            return await Task.FromResult(medicalRecords.Select(RegularMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
