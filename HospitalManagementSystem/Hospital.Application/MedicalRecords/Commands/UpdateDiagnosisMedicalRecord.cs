using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record UpdateDiagnosisMedicalRecord(int Id, int PatientId, int DoctorId,
       DateTimeOffset DateOfExamination, string ExaminationNotes, int IllnessId, int TreatmentId,
       string PrescribedMedicine, TimeSpan TreatmentDuration) : IRequest<DiagnosisMedicalRecordDto>;

    public class UpdateDiagnosisMedicalRecordHandler
        : IRequestHandler<UpdateDiagnosisMedicalRecord, DiagnosisMedicalRecordDto>
    {
        private readonly IDiagnosisMedicalRecordRepository _medicalRecordRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IIllnessRepository _illnessRepository;
        private readonly ITreatmentRepository _treatmentRepository;

        public UpdateDiagnosisMedicalRecordHandler(IDiagnosisMedicalRecordRepository medicalRecordRepository,
            IPatientRepository patientRepository, IDoctorRepository doctorRepository, 
            IIllnessRepository illnessRepository, ITreatmentRepository treatmentRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _illnessRepository = illnessRepository;
            _treatmentRepository = treatmentRepository;
        }

        public Task<DiagnosisMedicalRecordDto> Handle(UpdateDiagnosisMedicalRecord request, CancellationToken cancellationToken)
        {
            var result = _medicalRecordRepository.Update(new DiagnosisMedicalRecord()
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
            });

            if (result)
            {
                var updatedRecord = _medicalRecordRepository.GetById(request.Id);
                _treatmentRepository.Update(updatedRecord.ProposedTreatment);
                return Task.FromResult(DiagnosisMedicalRecordDto.FromMedicalRecord(updatedRecord));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing diagnosis medical record with id {request.Id}");
            }
        }
    }
}
