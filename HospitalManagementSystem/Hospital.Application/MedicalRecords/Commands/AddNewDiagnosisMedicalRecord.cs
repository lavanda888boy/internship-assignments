using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AddNewDiagnosisMedicalRecord(int PatientId, int DoctorId, string ExaminationNotes,
       int IllnessId, int TreatmentId, string PrescribedMedicine, TimeSpan Duration)
       : IRequest<DiagnosisMedicalRecordDto>;

    public class AddNewDiagnosisMedicalRecordHandler
        : IRequestHandler<AddNewDiagnosisMedicalRecord, DiagnosisMedicalRecordDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddNewDiagnosisMedicalRecordHandler(IUnitOfWork unitOFWork)
        {
            _unitOfWork = unitOFWork;
        }

        public async Task<DiagnosisMedicalRecordDto> Handle(AddNewDiagnosisMedicalRecord request,
            CancellationToken cancellationToken)
        {
            var examinedPatient = await _unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);
            var responsibleDoctor = await _unitOfWork.DoctorRepository.GetByIdAsync(request.DoctorId);
            var illness = await _unitOfWork.IllnessRepository.GetByIdAsync(request.IllnessId);

            if (examinedPatient == null)
            {
                throw new NoEntityFoundException($"Cannot create diagnosis medical record of a non-existing patient with id {request.PatientId}");
            }

            if (responsibleDoctor == null)
            {
                throw new NoEntityFoundException($"Cannot create diagnosis medical record for a non-existing doctor with id {request.DoctorId}");
            }

            if (illness == null)
            {
                throw new NoEntityFoundException($"Cannot create diagnosis medical record with a non-existing illness with id {request.IllnessId}");
            }

            bool examinedPatientIsAssignedToTheDoctor = examinedPatient.DoctorsPatients
                                                                        .Any(dp => dp.DoctorId == responsibleDoctor.Id);
            if (examinedPatientIsAssignedToTheDoctor)
            {
                var treatment = new Treatment()
                {
                    Id = request.TreatmentId,
                    PrescribedMedicine = request.PrescribedMedicine,
                    Duration = request.Duration,
                };

                await _unitOfWork.TreatmentRepository.AddAsync(treatment);

                var medicalRecord = new DiagnosisMedicalRecord
                {
                    ExaminedPatient = examinedPatient,
                    ResponsibleDoctor = responsibleDoctor,
                    DateOfExamination = DateTimeOffset.UtcNow,
                    ExaminationNotes = request.ExaminationNotes,
                    DiagnosedIllness = illness,
                    ProposedTreatment = treatment
                };

                var createdRecord = await _unitOfWork.DiagnosisRecordRepository.AddAsync(medicalRecord);

                return await Task.FromResult(DiagnosisMedicalRecordDto.FromMedicalRecord(createdRecord));
            }
            else
            {
                throw new PatientDoctorMisassignationException("Cannot create diagnosis medical record beacuse the patient is not assigned to the doctor");
            }
        }
    }
}
