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
    public record SearchDiagnosisMedicalRecordsByASetOfPropertiesPaginated(int PageNumber, int PageSize,
        int? ExaminedPatientId, int? ResponsibleDoctorId, DateTimeOffset? DateOfExamination, 
        string? DiagnosedIllnessName, string? PrescribedMedicine) : IRequest<PaginatedResult<DiagnosisMedicalRecordFullInfoDto>>;

    public class SearchDiagnosisMedicalRecordsByASetOfPropertiesPaginatedHandler
        : IRequestHandler<SearchDiagnosisMedicalRecordsByASetOfPropertiesPaginated, 
            PaginatedResult<DiagnosisMedicalRecordFullInfoDto>>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public SearchDiagnosisMedicalRecordsByASetOfPropertiesPaginatedHandler(IRepository<DiagnosisMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<DiagnosisMedicalRecordFullInfoDto>> Handle(SearchDiagnosisMedicalRecordsByASetOfPropertiesPaginated request,
            CancellationToken cancellationToken)
        {
            Expression<Func<DiagnosisMedicalRecord, bool>> predicate = r =>
                (request.ExaminedPatientId == 0 || r.ExaminedPatient.Id == request.ExaminedPatientId) &&
                (request.ResponsibleDoctorId == 0 || r.ResponsibleDoctor.Id == request.ResponsibleDoctorId) &&
                (!request.DateOfExamination.HasValue || r.DateOfExamination == request.DateOfExamination) &&
                (string.IsNullOrEmpty(request.DiagnosedIllnessName) || r.DiagnosedIllness.Name == request.DiagnosedIllnessName) &&
                (string.IsNullOrEmpty(request.PrescribedMedicine) || r.ProposedTreatment.PrescribedMedicine == request.PrescribedMedicine);

            var paginatedRecords = await _recordRepository.SearchByPropertyPaginatedAsync(predicate,
                request.PageNumber, request.PageSize);

            if (paginatedRecords.Items.Count == 0)
            {
                throw new NoEntityFoundException("No diagnosis medical records with such properties exist");
            }

            return await Task.FromResult(new PaginatedResult<DiagnosisMedicalRecordFullInfoDto>
            {
                TotalItems = paginatedRecords.TotalItems,
                Items = _mapper.Map<List<DiagnosisMedicalRecordFullInfoDto>>(paginatedRecords.Items)
            });
        }
    }
}
