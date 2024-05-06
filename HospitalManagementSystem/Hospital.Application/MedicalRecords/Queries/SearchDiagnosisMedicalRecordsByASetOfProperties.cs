using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record SearchDiagnosisMedicalRecordsByASetOfProperties(DiagnosisMedicalRecordFilters RecordFilters)
        : IRequest<List<DiagnosisMedicalRecordDto>>;

    public class SearchDiagnosisMedicalRecordsByASetOfPropertiesHandler
        : IRequestHandler<SearchDiagnosisMedicalRecordsByASetOfProperties, List<DiagnosisMedicalRecordDto>>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public SearchDiagnosisMedicalRecordsByASetOfPropertiesHandler(IRepository<DiagnosisMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
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

            var medicalRecordDtos = _mapper.Map<List<DiagnosisMedicalRecordDto>>(medicalRecords);
            return await Task.FromResult(medicalRecordDtos);
        }
    }
}
