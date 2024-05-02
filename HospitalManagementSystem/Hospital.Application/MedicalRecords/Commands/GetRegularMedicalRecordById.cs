using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record GetRegularMedicalRecordById(int RecordId) : IRequest<RegularMedicalRecordDto>;

    public class GetRegularMedicalRecordByIdHandler : IRequestHandler<GetRegularMedicalRecordById, RegularMedicalRecordDto>
    {
        private readonly IRepository<RegularMedicalRecord> _recordRepository;

        public GetRegularMedicalRecordByIdHandler(IRepository<RegularMedicalRecord> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<RegularMedicalRecordDto> Handle(GetRegularMedicalRecordById request, CancellationToken cancellationToken)
        {
            var record = await _recordRepository.GetByIdAsync(request.RecordId);

            if (record == null)
            {
                throw new NoEntityFoundException($"Diagnosis medical record with id {request.RecordId} does not exist");
            }

            return await Task.FromResult(RegularMedicalRecordDto.FromMedicalRecord(record));
        }
    }
}
