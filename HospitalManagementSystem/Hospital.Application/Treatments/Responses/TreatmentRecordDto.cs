namespace Hospital.Application.Treatments.Responses
{
    public class TreatmentRecordDto
    {
        public required string PrescribedMedicine { get; init; }
        public int DurationInDays { get; init; }
    }
}
