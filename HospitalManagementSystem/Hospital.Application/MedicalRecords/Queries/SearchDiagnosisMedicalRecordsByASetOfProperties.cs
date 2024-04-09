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
        private readonly IDiagnosisMedicalRecordRepository _medicalRecordRepository;

        public SearchDiagnosisMedicalRecordsByASetOfPropertiesHandler(IDiagnosisMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<List<DiagnosisMedicalRecordDto>> Handle(SearchDiagnosisMedicalRecordsByASetOfProperties request,
            CancellationToken cancellationToken)
        {
            Expression<Func<DiagnosisMedicalRecord, bool>> predicate = r =>
                (request.RecordFilters.ExaminedPatientId == 0 || r.ExaminedPatient.Id == request.RecordFilters.ExaminedPatientId) &&
                (request.RecordFilters.ResponsibleDoctorId == 0 || r.ResponsibleDoctor.Id == request.RecordFilters.ResponsibleDoctorId) &&
                (!request.RecordFilters.DateOfExamination.HasValue || r.DateOfExamination == request.RecordFilters.DateOfExamination) &&
                (string.IsNullOrEmpty(request.RecordFilters.DiagnosedIllnessName) || r.DiagnosedIllness.Name == request.RecordFilters.DiagnosedIllnessName) &&
                (string.IsNullOrEmpty(request.RecordFilters.PrescribedMedicine) || r.ProposedTreatment.PrescribedMedicine == request.RecordFilters.PrescribedMedicine)

            var medicalRecords = _medicalRecordRepository.SearchByProperty(predicate.Compile());

            if (medicalRecords is null)
            {
                throw new NoEntityFoundException("No diagnosis medical records with such properties exist");
            }

            return Task.FromResult(medicalRecords.Select(DiagnosisMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
