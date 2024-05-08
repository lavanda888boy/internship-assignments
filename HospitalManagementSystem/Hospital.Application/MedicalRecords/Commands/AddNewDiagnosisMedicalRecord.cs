using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AddNewDiagnosisMedicalRecord(int PatientId, int DoctorId, string ExaminationNotes,
       int IllnessId, string PrescribedMedicine, int Duration)
       : IRequest<int>;

    public class AddNewDiagnosisMedicalRecordHandler
        : IRequestHandler<AddNewDiagnosisMedicalRecord, int>
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Illness> _illnessRepository;
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;

        public AddNewDiagnosisMedicalRecordHandler(IRepository<Patient> patientRepository,
            IRepository<Doctor> doctorRepository,
            IRepository<Illness> illnessRepository,
            IRepository<DiagnosisMedicalRecord> recordRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _illnessRepository = illnessRepository;
            _recordRepository = recordRepository;
        }

        public async Task<int> Handle(AddNewDiagnosisMedicalRecord request,
            CancellationToken cancellationToken)
        {
            var examinedPatient = await _patientRepository.GetByIdAsync(request.PatientId);
            var responsibleDoctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            var illness = await _illnessRepository.GetByIdAsync(request.IllnessId);

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
                    PrescribedMedicine = request.PrescribedMedicine,
                    DurationInDays = request.Duration,
                };

                DateTimeOffset now = DateTimeOffset.UtcNow;

                var medicalRecord = new DiagnosisMedicalRecord
                {
                    ExaminedPatient = examinedPatient,
                    ResponsibleDoctor = responsibleDoctor,
                    DateOfExamination = new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, now.Offset),
                    ExaminationNotes = request.ExaminationNotes,
                    DiagnosedIllness = illness,
                    ProposedTreatment = treatment
                };

                var createdRecord = await _recordRepository.AddAsync(medicalRecord);

                return await Task.FromResult(createdRecord.Id);
            }
            else
            {
                throw new PatientDoctorMisassignationException("Cannot create diagnosis medical record because the patient is not assigned to the doctor");
            }
        }
    }
}
