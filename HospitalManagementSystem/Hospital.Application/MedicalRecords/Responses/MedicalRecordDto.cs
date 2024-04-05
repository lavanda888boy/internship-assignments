using Hospital.Domain.Models;

namespace Hospital.Application.MedicalRecords.Responses
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public required Patient ExaminedPatient { get; set; }
        public required Doctor ResponsibleDoctor { get; set; }
        public required DateTimeOffset DateOfExamination { get; set; }
        public required string ExaminationNotes { get; set; }
        public string? Diagnosis { get; set; }

        public static MedicalRecordDto FromMedicalRecord(MedicalRecord medicalRecord)
        {
            return new MedicalRecordDto()
            {
                Id = medicalRecord.Id,
                ExaminedPatient = medicalRecord.ExaminedPatient,
                ResponsibleDoctor = medicalRecord.ResponsibleDoctor,
                DateOfExamination = medicalRecord.DateOfExamination,
                ExaminationNotes = medicalRecord.ExaminationNotes,
                Diagnosis = medicalRecord.Diagnosis,
            };
        }
    }
}
