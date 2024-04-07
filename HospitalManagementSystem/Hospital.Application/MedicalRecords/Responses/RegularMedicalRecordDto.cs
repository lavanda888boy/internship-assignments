using Hospital.Domain.Models;

namespace Hospital.Application.MedicalRecords.Responses
{
    public class RegularMedicalRecordDto
    {
        public int Id { get; set; }
        public required Patient ExaminedPatient { get; set; }
        public required Doctor ResponsibleDoctor { get; set; }
        public required DateTimeOffset DateOfExamination { get; set; }
        public required string ExaminationNotes { get; set; }

        public static RegularMedicalRecordDto FromMedicalRecord(RegularMedicalRecord medicalRecord)
        {
            return new RegularMedicalRecordDto()
            {
                Id = medicalRecord.Id,
                ExaminedPatient = medicalRecord.ExaminedPatient,
                ResponsibleDoctor = medicalRecord.ResponsibleDoctor,
                DateOfExamination = medicalRecord.DateOfExamination,
                ExaminationNotes = medicalRecord.ExaminationNotes,
            };
        }
    }
}
