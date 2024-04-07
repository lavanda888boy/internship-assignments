using Hospital.Domain.Models;

namespace Hospital.Application.MedicalRecords.Responses
{
    public class DiagnosisMedicalRecordDto : RegularMedicalRecordDto
    {
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
