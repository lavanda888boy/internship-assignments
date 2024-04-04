namespace Hospital.Domain.Models
{
    public class Illness
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required DateTimeOffset DateOfDiagnosis { get; set; }
        public DateTimeOffset? DateOfTreatmentEnd { get; set; }
        public required Doctor DiagnosisDoctor { get; set; }
    }
}
