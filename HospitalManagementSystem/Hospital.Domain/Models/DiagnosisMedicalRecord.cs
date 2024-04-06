namespace Hospital.Domain.Models
{
    public class DiagnosisMedicalRecord : RegularMedicalRecord
    {
        public required Illness DiagnosedIllness { get; set; }
        public required Treatment ProposedTreatment { get; set; }
    }
}
