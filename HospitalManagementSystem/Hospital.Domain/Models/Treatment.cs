namespace Hospital.Domain.Models
{
    public class Treatment
    {
        public required int Id { get; set; }
        public required string PrescribedMedicine { get; set; }
        public TimeSpan TreatmentDuration { get; set; }
    }
}
