namespace Hospital.Application.MedicalRecords.Responses
{
    public class DiagnosisMedicalRecordFilterDto : RegularMedicalRecordFilterDto
    {
        public string? DiagnosedIllnessName { get; set; }
        public string? PrescribedMedicine { get; set; }
    }
}
