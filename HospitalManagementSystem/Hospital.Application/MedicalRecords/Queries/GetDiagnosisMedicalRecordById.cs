using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record GetDiagnosisMedicalRecordById(int RecordId) : IRequest<DiagnosisMedicalRecordDto>;

    public class GetDiagnosisMedicalRecordByIdHandler : IRequestHandler<GetDiagnosisMedicalRecordById, DiagnosisMedicalRecordDto>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public GetDiagnosisMedicalRecordByIdHandler(IRepository<DiagnosisMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<DiagnosisMedicalRecordDto> Handle(GetDiagnosisMedicalRecordById request, CancellationToken cancellationToken)
        {
            var record = await _recordRepository.GetByIdAsync(request.RecordId);

            if (record == null)
            {
                throw new NoEntityFoundException($"Regular medical record with id {request.RecordId} does not exist");
            }

            return await Task.FromResult(_mapper.Map<DiagnosisMedicalRecordDto>(record));
        }
    }
}
