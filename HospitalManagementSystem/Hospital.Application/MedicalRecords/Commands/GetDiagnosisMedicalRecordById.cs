using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record GetDiagnosisMedicalRecordById(int RecordId) : IRequest<DiagnosisMedicalRecordDto>;

    public class GetDiagnosisMedicalRecordByIdHandler : IRequestHandler<GetDiagnosisMedicalRecordById, DiagnosisMedicalRecordDto>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;

        public GetDiagnosisMedicalRecordByIdHandler(IRepository<DiagnosisMedicalRecord> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<DiagnosisMedicalRecordDto> Handle(GetDiagnosisMedicalRecordById request, CancellationToken cancellationToken)
        {
            var record = await _recordRepository.GetByIdAsync(request.RecordId);

            if (record == null)
            {
                throw new NoEntityFoundException($"Regular medical record with id {request.RecordId} does not exist");
            }

            return await Task.FromResult(DiagnosisMedicalRecordDto.FromMedicalRecord(record));
        }
    }
}
