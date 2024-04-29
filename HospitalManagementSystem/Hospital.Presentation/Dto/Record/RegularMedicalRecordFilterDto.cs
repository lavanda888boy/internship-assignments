namespace Hospital.Presentation.Dto.Record
{
    public class RegularMedicalRecordFilterDto
    {
        public int ExaminedPatientId { get; set; }
        public int ResponsibleDoctorId { get; set; }
        public DateTimeOffset? DateOfExamination { get; set; }
    }
}
