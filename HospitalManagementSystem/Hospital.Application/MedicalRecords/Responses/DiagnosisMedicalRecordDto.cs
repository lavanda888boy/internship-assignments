using Hospital.Domain.Models;

namespace Hospital.Application.MedicalRecords.Responses
{
    public class DiagnosisMedicalRecordDto
    {
        public int Id { get; set; }
        public required Patient ExaminedPatient { get; set; }
        public required Doctor ResponsibleDoctor { get; set; }
        public required DateTimeOffset DateOfExamination { get; set; }
        public required string ExaminationNotes { get; set; }
        public required Illness DiagnosedIllness { get; set; }
        public required Treatment ProposedTreatment { get; set; }

        public static DiagnosisMedicalRecordDto FromMedicalRecord(DiagnosisMedicalRecord medicalRecord)
        {
            return new DiagnosisMedicalRecordDto()
            {
                Id = medicalRecord.Id,
                ExaminedPatient = medicalRecord.ExaminedPatient,
                ResponsibleDoctor = medicalRecord.ResponsibleDoctor,
                DateOfExamination = medicalRecord.DateOfExamination,
                ExaminationNotes = medicalRecord.ExaminationNotes,
                DiagnosedIllness = medicalRecord.DiagnosedIllness,
                ProposedTreatment = medicalRecord.ProposedTreatment,
            };
        }
    }
}
