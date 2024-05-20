using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record ListAllPaginatedDiagnosisMedicalRecords(int PageNumber, int PageSize) 
        : IRequest<PaginatedResult<DiagnosisMedicalRecordFullInfoDto>>;

    public class ListAllPaginatedDiagnosisMedicalRecordsHandler 
        : IRequestHandler<ListAllPaginatedDiagnosisMedicalRecords, PaginatedResult<DiagnosisMedicalRecordFullInfoDto>>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public ListAllPaginatedDiagnosisMedicalRecordsHandler(IRepository<DiagnosisMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<DiagnosisMedicalRecordFullInfoDto>> Handle(ListAllPaginatedDiagnosisMedicalRecords request,
            CancellationToken cancellationToken)
        {
            var paginatedRecords = await _recordRepository.GetAllPaginatedAsync(request.PageNumber, request.PageSize);
            return await Task.FromResult(new PaginatedResult<DiagnosisMedicalRecordFullInfoDto>
            {
                TotalItems = paginatedRecords.TotalItems,
                Items = _mapper.Map<List<DiagnosisMedicalRecordFullInfoDto>>(paginatedRecords.Items)
        });
        }
    }
}
