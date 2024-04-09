namespace Hospital.Application.MedicalRecords.Responses
{
    public class RegularMedicalRecordFilterDto
    {
        public int ExaminedPatientId { get; set; }
        public int ResponsibleDoctorId { get; set; }
        public DateTimeOffset? DateOfExamination { get; set; }
    }
}
