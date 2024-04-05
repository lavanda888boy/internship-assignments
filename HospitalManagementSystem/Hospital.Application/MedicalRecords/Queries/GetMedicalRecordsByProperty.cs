using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record GetMedicalRecordsByProperty(Func<MedicalRecord, bool> MedicalRecordProperty)
        : IRequest<List<MedicalRecordDto>>;

    public class GetMedicalRecordsByPropertyHandler
        : IRequestHandler<GetMedicalRecordsByProperty, List<MedicalRecordDto>>
    {
        private readonly IRepository<MedicalRecord> _medicalRecordRepository;

        public GetMedicalRecordsByPropertyHandler(IRepository<MedicalRecord> medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<List<MedicalRecordDto>> Handle(GetMedicalRecordsByProperty request, CancellationToken cancellationToken)
        {
            var medicalRecords = _medicalRecordRepository.GetByProperty(request.MedicalRecordProperty);

            if (medicalRecords is null)
            {
                throw new NoEntityFoundException("No medical records with such properties exist");
            }

            return Task.FromResult(medicalRecords.Select(MedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
