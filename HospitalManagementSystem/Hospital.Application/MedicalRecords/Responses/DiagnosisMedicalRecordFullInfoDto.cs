using Hospital.Application.Doctors.Responses;
using Hospital.Application.Illnesses.Responses;
using Hospital.Application.Patients.Responses;
using Hospital.Application.Treatments.Responses;
using Hospital.Domain.Models;

namespace Hospital.Application.MedicalRecords.Responses
{
    public class DiagnosisMedicalRecordFullInfoDto
    {
        public int Id { get; init; }
        public required PatientShortInfoDto ExaminedPatient { get; init; }
        public required DoctorShortInfoDto ResponsibleDoctor { get; init; }
        public required DateTimeOffset DateOfExamination { get; init; }
        public required string ExaminationNotes { get; init; }
        public required IllnessRecordDto DiagnosedIllness { get; init; }
        public required TreatmentRecordDto ProposedTreatment { get; init; }
    }
}
