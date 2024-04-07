using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record CreateDiagnosisMedicalRecord(int Id, int PatientId, int DoctorId,
       DateTimeOffset DateOfExamination, string ExaminationNotes, int IllnessId, int TreatmentId,
       string PrescribedMedicine, TimeSpan TreatmentDuration) : IRequest<DiagnosisMedicalRecordDto>;

    public class CreateDiagnosisMedicalRecordHandler
        : IRequestHandler<CreateDiagnosisMedicalRecord, DiagnosisMedicalRecordDto>
    {
        private readonly IDiagnosisMedicalRecordRepository _medicalRecordRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IIllnessRepository _illnessRepository;
        private readonly ITreatmentRepository _treatmentRepository;

        public CreateDiagnosisMedicalRecordHandler(IDiagnosisMedicalRecordRepository medicalRecordRepository,
            IPatientRepository patientRepository, IDoctorRepository doctorRepository, 
            IIllnessRepository illnessRepository, ITreatmentRepository treatmentRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _illnessRepository = illnessRepository;
            _treatmentRepository = treatmentRepository;
        }

        public Task<DiagnosisMedicalRecordDto> Handle(CreateDiagnosisMedicalRecord request,
            CancellationToken cancellationToken)
        {
            var medicalRecord = new DiagnosisMedicalRecord
            {
                Id = request.Id,
                ExaminedPatient = _patientRepository.GetById(request.PatientId),
                ResponsibleDoctor = _doctorRepository.GetById(request.DoctorId),
                DateOfExamination = request.DateOfExamination,
                ExaminationNotes = request.ExaminationNotes,
                DiagnosedIllness = _illnessRepository.GetById(request.IllnessId),
                ProposedTreatment = new Treatment()
                {
                    Id = request.TreatmentId,
                    PrescribedMedicine = request.PrescribedMedicine,
                    TreatmentDuration = request.TreatmentDuration,
                }
            };

            _treatmentRepository.Create(medicalRecord.ProposedTreatment);

            var createdRecord = _medicalRecordRepository.Create(medicalRecord);
            return Task.FromResult(DiagnosisMedicalRecordDto.FromMedicalRecord(createdRecord));
        }
    }
}
