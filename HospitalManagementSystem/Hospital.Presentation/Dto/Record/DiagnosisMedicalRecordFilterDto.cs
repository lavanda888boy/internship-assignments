namespace Hospital.Presentation.Dto.Record
{
    public class DiagnosisMedicalRecordFilterDto
    {
        public int ExaminedPatientId { get; set; }
        public int ResponsibleDoctorId { get; set; }
        public DateTimeOffset? DateOfExamination { get; set; }
        public string? DiagnosedIllnessName { get; set; }
        public string? PrescribedMedicine { get; set; }
    }
}
