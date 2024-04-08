using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record ListAllRegularMedicalRecords()
        : IRequest<List<RegularMedicalRecordDto>>;

    public class ListAllRegularMedicalRecordsHandler
        : IRequestHandler<ListAllRegularMedicalRecords, List<RegularMedicalRecordDto>>
    {
        private readonly IRegularMedicalRecordRepository _medicalRecordRepository;

        public ListAllRegularMedicalRecordsHandler(IRegularMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<List<RegularMedicalRecordDto>> Handle(ListAllRegularMedicalRecords request, CancellationToken cancellationToken)
        {
            var medicalRecords = _medicalRecordRepository.GetAll();
            return Task.FromResult(medicalRecords.Select(RegularMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
