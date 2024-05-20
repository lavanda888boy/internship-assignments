using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record ListAllPaginatedRegularMedicalRecords(int PageNumber, int PageSize)
        : IRequest<PaginatedResult<RegularMedicalRecordFullInfoDto>>;

    public class ListAllPaginatedRegularMedicalRecordsHandler
        : IRequestHandler<ListAllPaginatedRegularMedicalRecords, PaginatedResult<RegularMedicalRecordFullInfoDto>>
    {
        private readonly IRepository<RegularMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public ListAllPaginatedRegularMedicalRecordsHandler(IRepository<RegularMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<RegularMedicalRecordFullInfoDto>> Handle(ListAllPaginatedRegularMedicalRecords request, CancellationToken cancellationToken)
        {
            var paginatedRecords = await _recordRepository.GetAllPaginatedAsync(request.PageNumber, request.PageSize);
            return await Task.FromResult(new PaginatedResult<RegularMedicalRecordFullInfoDto>
            {
                TotalItems = paginatedRecords.TotalItems,
                Items = _mapper.Map<List<RegularMedicalRecordFullInfoDto>>(paginatedRecords.Items)
            });
        }
    }
}
