using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record GetRegularMedicalRecordsByProperty(Func<RegularMedicalRecord, bool> MedicalRecordProperty)
        : IRequest<List<RegularMedicalRecordDto>>;

    public class GetRegularMedicalRecordsByPropertyHandler
        : IRequestHandler<GetRegularMedicalRecordsByProperty, List<RegularMedicalRecordDto>>
    {
        private readonly IRegularMedicalRecordRepository _medicalRecordRepository;

        public GetRegularMedicalRecordsByPropertyHandler(IRegularMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<List<RegularMedicalRecordDto>> Handle(GetRegularMedicalRecordsByProperty request, CancellationToken cancellationToken)
        {
            var medicalRecords = _medicalRecordRepository.SearchByProperty(request.MedicalRecordProperty);

            if (medicalRecords is null)
            {
                throw new NoEntityFoundException("No medical records with such properties exist");
            }

            return Task.FromResult(medicalRecords.Select(RegularMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
