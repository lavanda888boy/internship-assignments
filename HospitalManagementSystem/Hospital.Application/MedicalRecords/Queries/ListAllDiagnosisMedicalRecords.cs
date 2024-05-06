using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record ListAllDiagnosisMedicalRecords() : IRequest<List<DiagnosisMedicalRecordDto>>;

    public class ListAllDiagnosisMedicalRecordsHandler 
        : IRequestHandler<ListAllDiagnosisMedicalRecords, List<DiagnosisMedicalRecordDto>>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public ListAllDiagnosisMedicalRecordsHandler(IRepository<DiagnosisMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<List<DiagnosisMedicalRecordDto>> Handle(ListAllDiagnosisMedicalRecords request,
            CancellationToken cancellationToken)
        {
            var medicalRecords = await _recordRepository.GetAllAsync();
            var medicalRecordDtos = _mapper.Map<List<DiagnosisMedicalRecordDto>>(medicalRecords);
            return await Task.FromResult(medicalRecordDtos);
        }
    }
}
