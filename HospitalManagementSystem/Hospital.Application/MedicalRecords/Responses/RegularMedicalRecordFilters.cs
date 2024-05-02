namespace Hospital.Application.MedicalRecords.Responses
{
    public class RegularMedicalRecordFilters
    {
        public int ExaminedPatientId { get; set; }
        public int ResponsibleDoctorId { get; set; }
        public DateTimeOffset? DateOfExamination { get; set; }
    }
}
