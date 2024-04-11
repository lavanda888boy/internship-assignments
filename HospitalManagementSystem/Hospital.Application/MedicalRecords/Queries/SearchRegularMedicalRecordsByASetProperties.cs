using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record SearchRegularMedicalRecordsByASetProperties(RegularMedicalRecordFilterDto RecordFilters)
        : IRequest<List<RegularMedicalRecordDto>>;

    public class SearchRegularMedicalRecordsByASetPropertiesHandler
        : IRequestHandler<SearchRegularMedicalRecordsByASetProperties, List<RegularMedicalRecordDto>>
    {
        private readonly IRegularMedicalRecordRepository _medicalRecordRepository;

        public SearchRegularMedicalRecordsByASetPropertiesHandler(IRegularMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<List<RegularMedicalRecordDto>> Handle(SearchRegularMedicalRecordsByASetProperties request, CancellationToken cancellationToken)
        {
            Expression<Func<RegularMedicalRecord, bool>> predicate = r =>
                (request.RecordFilters.ExaminedPatientId == 0 || r.ExaminedPatient.Id == request.RecordFilters.ExaminedPatientId) &&
                (request.RecordFilters.ResponsibleDoctorId == 0 || r.ResponsibleDoctor.Id == request.RecordFilters.ResponsibleDoctorId) &&
                (!request.RecordFilters.DateOfExamination.HasValue || r.DateOfExamination == request.RecordFilters.DateOfExamination);

            var medicalRecords = _medicalRecordRepository.SearchByProperty(predicate.Compile());

            if (medicalRecords.Count == 0)
            {
                throw new NoEntityFoundException("No regular medical records with such properties exist");
            }

            return Task.FromResult(medicalRecords.Select(RegularMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
