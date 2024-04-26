using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record SearchDiagnosisMedicalRecordsByASetOfProperties(DiagnosisMedicalRecordFilterDto RecordFilters)
        : IRequest<List<DiagnosisMedicalRecordDto>>;

    public class SearchDiagnosisMedicalRecordsByASetOfPropertiesHandler
        : IRequestHandler<SearchDiagnosisMedicalRecordsByASetOfProperties, List<DiagnosisMedicalRecordDto>>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;

        public SearchDiagnosisMedicalRecordsByASetOfPropertiesHandler(IRepository<DiagnosisMedicalRecord> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<List<DiagnosisMedicalRecordDto>> Handle(SearchDiagnosisMedicalRecordsByASetOfProperties request,
            CancellationToken cancellationToken)
        {
            Expression<Func<DiagnosisMedicalRecord, bool>> predicate = r =>
                (request.RecordFilters.ExaminedPatientId == 0 || r.ExaminedPatient.Id == request.RecordFilters.ExaminedPatientId) &&
                (request.RecordFilters.ResponsibleDoctorId == 0 || r.ResponsibleDoctor.Id == request.RecordFilters.ResponsibleDoctorId) &&
                (!request.RecordFilters.DateOfExamination.HasValue || r.DateOfExamination == request.RecordFilters.DateOfExamination) &&
                (string.IsNullOrEmpty(request.RecordFilters.DiagnosedIllnessName) || r.DiagnosedIllness.Name == request.RecordFilters.DiagnosedIllnessName) &&
                (string.IsNullOrEmpty(request.RecordFilters.PrescribedMedicine) || r.ProposedTreatment.PrescribedMedicine == request.RecordFilters.PrescribedMedicine);

            var medicalRecords = await _recordRepository.SearchByPropertyAsync(predicate);

            if (medicalRecords.Count == 0)
            {
                throw new NoEntityFoundException("No diagnosis medical records with such properties exist");
            }

            return await Task.FromResult(medicalRecords.Select(DiagnosisMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
