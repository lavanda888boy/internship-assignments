using Hospital.Application.Doctors.Responses;
using Hospital.Application.Patients.Responses;

namespace Hospital.Application.MedicalRecords.Responses
{
    public class RegularMedicalRecordFullInfoDto
    {
        public int Id { get; init; }
        public required PatientShortInfoDto ExaminedPatient { get; init; }
        public required DoctorShortInfoDto ResponsibleDoctor { get; init; }
        public required DateTimeOffset DateOfExamination { get; init; }
        public required string ExaminationNotes { get; init; }
    }
}
