using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record ListAllRegularMedicalRecords()
        : IRequest<List<RegularMedicalRecordDto>>;

    public class ListAllRegularMedicalRecordsHandler
        : IRequestHandler<ListAllRegularMedicalRecords, List<RegularMedicalRecordDto>>
    {
        private readonly IRepository<RegularMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public ListAllRegularMedicalRecordsHandler(IRepository<RegularMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<List<RegularMedicalRecordDto>> Handle(ListAllRegularMedicalRecords request, CancellationToken cancellationToken)
        {
            var medicalRecords = await _recordRepository.GetAllAsync();
            var medicalRecordDtos = _mapper.Map<List<RegularMedicalRecordDto>>(medicalRecords);
            return await Task.FromResult(medicalRecordDtos);
        }
    }
}
