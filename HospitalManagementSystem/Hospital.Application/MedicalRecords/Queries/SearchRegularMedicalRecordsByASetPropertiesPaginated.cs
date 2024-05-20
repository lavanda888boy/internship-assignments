using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record SearchRegularMedicalRecordsByASetPropertiesPaginated(int PageNumber, int PageSize,
        int? ExaminedPatientId, int? ResponsibleDoctorId, DateTimeOffset? DateOfExamination) 
        : IRequest<PaginatedResult<RegularMedicalRecordFullInfoDto>>;

    public class SearchRegularMedicalRecordsByASetPropertiesPaginatedHandler
        : IRequestHandler<SearchRegularMedicalRecordsByASetPropertiesPaginated,
            PaginatedResult<RegularMedicalRecordFullInfoDto>>
    {
        private readonly IRepository<RegularMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public SearchRegularMedicalRecordsByASetPropertiesPaginatedHandler(IRepository<RegularMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<RegularMedicalRecordFullInfoDto>> Handle(SearchRegularMedicalRecordsByASetPropertiesPaginated request, CancellationToken cancellationToken)
        {
            Expression<Func<RegularMedicalRecord, bool>> predicate = r =>
                (request.ExaminedPatientId == 0 || r.ExaminedPatient.Id == request.ExaminedPatientId) &&
                (request.ResponsibleDoctorId == 0 || r.ResponsibleDoctor.Id == request.ResponsibleDoctorId) &&
                (!request.DateOfExamination.HasValue || r.DateOfExamination == request.DateOfExamination);

            var paginatedRecords = await _recordRepository.SearchByPropertyPaginatedAsync(predicate,
                request.PageNumber, request.PageSize);

            if (paginatedRecords.Items.Count == 0)
            {
                throw new NoEntityFoundException("No regular medical records with such properties exist");
            }

            return await Task.FromResult(new PaginatedResult<RegularMedicalRecordFullInfoDto>
            {
                TotalItems = paginatedRecords.TotalItems,
                Items = _mapper.Map<List<RegularMedicalRecordFullInfoDto>>(paginatedRecords.Items)
            });
        }
    }
}
