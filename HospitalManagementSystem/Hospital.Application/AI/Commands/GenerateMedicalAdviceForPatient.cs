using Hospital.Application.Abstractions;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.AI.Commands
{
    public record GenerateMedicalAdviceForPatient(int PatientId) : IRequest<string>;

    public class GenerateMedicalAdviceForPatientHandler : IRequestHandler<GenerateMedicalAdviceForPatient, string>
    {
        private readonly int _maxNumberOfSearchedRecords = 10;

        private readonly IRepository<DiagnosisMedicalRecord> _diagnosisRecordRepository;
        private readonly IMedicalAdviceGenerationService _medicalAdviceGenerationService;

        public GenerateMedicalAdviceForPatientHandler(IRepository<DiagnosisMedicalRecord> diagnosisMedicalRecordRepository, 
            IMedicalAdviceGenerationService medicalAdviceGenerationService)
        {
            _diagnosisRecordRepository = diagnosisMedicalRecordRepository;
            _medicalAdviceGenerationService = medicalAdviceGenerationService;
        }

        public async Task<string> Handle(GenerateMedicalAdviceForPatient request, CancellationToken cancellationToken)
        {
            Expression<Func<DiagnosisMedicalRecord, bool>> predicate = r => r.ExaminedPatient.Id == request.PatientId;
            var patientRecentIllnesses = (await _diagnosisRecordRepository.SearchByPropertyPaginatedAsync(predicate, 1, _maxNumberOfSearchedRecords)).Items
                                            .Select(r => r.DiagnosedIllness.Name)
                                            .ToList();

            var advice = await _medicalAdviceGenerationService.GenerateMedicalAdviceForPatient(patientRecentIllnesses);
            return advice;
        }
    }
}
