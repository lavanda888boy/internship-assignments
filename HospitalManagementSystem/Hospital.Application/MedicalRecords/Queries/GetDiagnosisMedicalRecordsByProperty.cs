using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record GetDiagnosisMedicalRecordsByProperty(Func<DiagnosisMedicalRecord, bool> MedicalRecordProperty)
        : IRequest<List<DiagnosisMedicalRecordDto>>;

    public class GetDiagnosisMedicalRecordsByPropertyHandler
        : IRequestHandler<GetDiagnosisMedicalRecordsByProperty, List<DiagnosisMedicalRecordDto>>
    {
        private readonly IDiagnosisMedicalRecordRepository _medicalRecordRepository;

        public GetDiagnosisMedicalRecordsByPropertyHandler(IDiagnosisMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<List<DiagnosisMedicalRecordDto>> Handle(GetDiagnosisMedicalRecordsByProperty request,
            CancellationToken cancellationToken)
        {
            var medicalRecords = _medicalRecordRepository.SearchByProperty(request.MedicalRecordProperty);

            if (medicalRecords is null)
            {
                throw new NoEntityFoundException("No medical records with such properties exist");
            }

            return Task.FromResult(medicalRecords.Select(DiagnosisMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
