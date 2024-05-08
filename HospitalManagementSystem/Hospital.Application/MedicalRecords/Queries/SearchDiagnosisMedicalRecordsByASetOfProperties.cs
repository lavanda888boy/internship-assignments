using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record SearchDiagnosisMedicalRecordsByASetOfProperties(int? ExaminedPatientId, int? ResponsibleDoctorId,
        DateTimeOffset? DateOfExamination, string? DiagnosedIllnessName, string? PrescribedMedicine)
        : IRequest<List<DiagnosisMedicalRecordFullInfoDto>>;

    public class SearchDiagnosisMedicalRecordsByASetOfPropertiesHandler
        : IRequestHandler<SearchDiagnosisMedicalRecordsByASetOfProperties, List<DiagnosisMedicalRecordFullInfoDto>>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public SearchDiagnosisMedicalRecordsByASetOfPropertiesHandler(IRepository<DiagnosisMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<List<DiagnosisMedicalRecordFullInfoDto>> Handle(SearchDiagnosisMedicalRecordsByASetOfProperties request,
            CancellationToken cancellationToken)
        {
            Expression<Func<DiagnosisMedicalRecord, bool>> predicate = r =>
                (request.ExaminedPatientId == 0 || r.ExaminedPatient.Id == request.ExaminedPatientId) &&
                (request.ResponsibleDoctorId == 0 || r.ResponsibleDoctor.Id == request.ResponsibleDoctorId) &&
                (!request.DateOfExamination.HasValue || r.DateOfExamination == request.DateOfExamination) &&
                (string.IsNullOrEmpty(request.DiagnosedIllnessName) || r.DiagnosedIllness.Name == request.DiagnosedIllnessName) &&
                (string.IsNullOrEmpty(request.PrescribedMedicine) || r.ProposedTreatment.PrescribedMedicine == request.PrescribedMedicine);

            var medicalRecords = await _recordRepository.SearchByPropertyAsync(predicate);

            if (medicalRecords.Count == 0)
            {
                throw new NoEntityFoundException("No diagnosis medical records with such properties exist");
            }

            var medicalRecordDtos = _mapper.Map<List<DiagnosisMedicalRecordFullInfoDto>>(medicalRecords);
            return await Task.FromResult(medicalRecordDtos);
        }
    }
}
