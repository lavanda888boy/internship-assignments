using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record GetRegularMedicalRecordById(int MedicalRecordId) : IRequest<RegularMedicalRecordDto>;

    public class GetMedicalRecordByIdHandler
        : IRequestHandler<GetRegularMedicalRecordById, RegularMedicalRecordDto>
    {
        private readonly IRegularMedicalRecordRepository _medicalRecordRepository;

        public GetMedicalRecordByIdHandler(IRegularMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<RegularMedicalRecordDto> Handle(GetRegularMedicalRecordById request, CancellationToken cancellationToken)
        {
            var medicalRecord = _medicalRecordRepository.GetById(request.MedicalRecordId);

            if (medicalRecord is null)
            {
                throw new NoEntityFoundException($"There is no medical record with id {request.MedicalRecordId}");
            }

            return Task.FromResult(RegularMedicalRecordDto.FromMedicalRecord(medicalRecord));
        }
    }
}
