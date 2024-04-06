namespace Hospital.Domain.Models
{
    public class RegularMedicalRecord
    {
        public required int Id { get; set; }
        public required Patient ExaminedPatient { get; set; }
        public required Doctor ResponsibleDoctor { get; set; }
        public required DateTimeOffset DateOfExamination { get; set; }
        public required string ExaminationNotes { get; set; }
    }
}
