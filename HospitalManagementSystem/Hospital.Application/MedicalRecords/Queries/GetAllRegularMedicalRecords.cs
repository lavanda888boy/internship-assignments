using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record GetAllRegularMedicalRecords()
        : IRequest<List<RegularMedicalRecordDto>>;

    public class GetAllMedicalRecordsHandler
        : IRequestHandler<GetAllRegularMedicalRecords, List<RegularMedicalRecordDto>>
    {
        private readonly IRegularMedicalRecordRepository _medicalRecordRepository;

        public GetAllMedicalRecordsHandler(IRegularMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<List<RegularMedicalRecordDto>> Handle(GetAllRegularMedicalRecords request, CancellationToken cancellationToken)
        {
            var medicalRecords = _medicalRecordRepository.GetAll();
            return Task.FromResult(medicalRecords.Select(RegularMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
