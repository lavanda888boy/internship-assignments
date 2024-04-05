using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record GetMedicalRecordById(int MedicalRecordId) : IRequest<MedicalRecordDto>;

    public class GetMedicalRecordByIdHandler
        : IRequestHandler<GetMedicalRecordById, MedicalRecordDto>
    {
        private readonly IRepository<MedicalRecord> _medicalRecordRepository;

        public GetMedicalRecordByIdHandler(IRepository<MedicalRecord> medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<MedicalRecordDto> Handle(GetMedicalRecordById request, CancellationToken cancellationToken)
        {
            var medicalRecord = _medicalRecordRepository.GetById(request.MedicalRecordId);

            if (medicalRecord is null)
            {
                throw new NoEntityFoundException($"There is no medical record with id {request.MedicalRecordId}");
            }

            return Task.FromResult(MedicalRecordDto.FromMedicalRecord(medicalRecord));
        }
    }
}
