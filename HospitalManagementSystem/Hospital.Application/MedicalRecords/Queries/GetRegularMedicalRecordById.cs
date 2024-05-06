using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record GetRegularMedicalRecordById(int RecordId) : IRequest<RegularMedicalRecordDto>;

    public class GetRegularMedicalRecordByIdHandler : IRequestHandler<GetRegularMedicalRecordById, RegularMedicalRecordDto>
    {
        private readonly IRepository<RegularMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public GetRegularMedicalRecordByIdHandler(IRepository<RegularMedicalRecord> recordRepository, 
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<RegularMedicalRecordDto> Handle(GetRegularMedicalRecordById request, CancellationToken cancellationToken)
        {
            var record = await _recordRepository.GetByIdAsync(request.RecordId);

            if (record == null)
            {
                throw new NoEntityFoundException($"Diagnosis medical record with id {request.RecordId} does not exist");
            }

            var recordDto = _mapper.Map<RegularMedicalRecordDto>(record);
            return await Task.FromResult(recordDto);
        }
    }
}
