using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record GetDiagnosisMedicalRecordById(int MedicalRecordId) 
        : IRequest<DiagnosisMedicalRecordDto>;

    public class GetDiagnosisMedicalRecordByIdHandler
        : IRequestHandler<GetDiagnosisMedicalRecordById, DiagnosisMedicalRecordDto>
    {
        private readonly IDiagnosisMedicalRecordRepository _medicalRecordRepository;

        public GetDiagnosisMedicalRecordByIdHandler(IDiagnosisMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<DiagnosisMedicalRecordDto> Handle(GetDiagnosisMedicalRecordById request,
            CancellationToken cancellationToken)
        {
            var medicalRecord = _medicalRecordRepository.GetById(request.MedicalRecordId);

            if (medicalRecord is null)
            {
                throw new NoEntityFoundException($"There is no diagnosis medical record with id {request.MedicalRecordId}");
            }

            return Task.FromResult(DiagnosisMedicalRecordDto.FromMedicalRecord(medicalRecord));
        }
    }
}
