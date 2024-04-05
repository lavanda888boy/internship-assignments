using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record GetAllMedicalRecords()
        : IRequest<List<MedicalRecordDto>>;

    public class GetAllMedicalRecordsHandler
        : IRequestHandler<GetAllMedicalRecords, List<MedicalRecordDto>>
    {
        private readonly IRepository<MedicalRecord> _medicalRecordRepository;

        public GetAllMedicalRecordsHandler(IRepository<MedicalRecord> medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<List<MedicalRecordDto>> Handle(GetAllMedicalRecords request, CancellationToken cancellationToken)
        {
            var medicalRecords = _medicalRecordRepository.GetAll();
            return Task.FromResult(medicalRecords.Select(MedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
