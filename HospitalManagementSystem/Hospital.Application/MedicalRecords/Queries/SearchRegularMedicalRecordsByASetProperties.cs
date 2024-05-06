using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record SearchRegularMedicalRecordsByASetProperties(RegularMedicalRecordFilters RecordFilters)
        : IRequest<List<RegularMedicalRecordDto>>;

    public class SearchRegularMedicalRecordsByASetPropertiesHandler
        : IRequestHandler<SearchRegularMedicalRecordsByASetProperties, List<RegularMedicalRecordDto>>
    {
        private readonly IRepository<RegularMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public SearchRegularMedicalRecordsByASetPropertiesHandler(IRepository<RegularMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<List<RegularMedicalRecordDto>> Handle(SearchRegularMedicalRecordsByASetProperties request, CancellationToken cancellationToken)
        {
            Expression<Func<RegularMedicalRecord, bool>> predicate = r =>
                (request.RecordFilters.ExaminedPatientId == 0 || r.ExaminedPatient.Id == request.RecordFilters.ExaminedPatientId) &&
                (request.RecordFilters.ResponsibleDoctorId == 0 || r.ResponsibleDoctor.Id == request.RecordFilters.ResponsibleDoctorId) &&
                (!request.RecordFilters.DateOfExamination.HasValue || r.DateOfExamination == request.RecordFilters.DateOfExamination);

            var medicalRecords = await _recordRepository.SearchByPropertyAsync(predicate);

            if (medicalRecords.Count == 0)
            {
                throw new NoEntityFoundException("No regular medical records with such properties exist");
            }

            var medicalRecordDtos = _mapper.Map<List<RegularMedicalRecordDto>>(medicalRecords);
            return await Task.FromResult(medicalRecordDtos);
        }
    }
}
