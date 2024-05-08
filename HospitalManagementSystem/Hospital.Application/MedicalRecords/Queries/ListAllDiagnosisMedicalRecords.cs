using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record ListAllDiagnosisMedicalRecords() : IRequest<List<DiagnosisMedicalRecordFullInfoDto>>;

    public class ListAllDiagnosisMedicalRecordsHandler 
        : IRequestHandler<ListAllDiagnosisMedicalRecords, List<DiagnosisMedicalRecordFullInfoDto>>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public ListAllDiagnosisMedicalRecordsHandler(IRepository<DiagnosisMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<List<DiagnosisMedicalRecordFullInfoDto>> Handle(ListAllDiagnosisMedicalRecords request,
            CancellationToken cancellationToken)
        {
            var medicalRecords = await _recordRepository.GetAllAsync();
            var medicalRecordDtos = _mapper.Map<List<DiagnosisMedicalRecordFullInfoDto>>(medicalRecords);
            return await Task.FromResult(medicalRecordDtos);
        }
    }
}
