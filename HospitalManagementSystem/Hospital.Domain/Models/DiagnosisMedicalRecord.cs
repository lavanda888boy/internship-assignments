using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Domain.Models
{
    public class DiagnosisMedicalRecord
    {
        [Column("RecordId")]
        public int Id { get; set; }

        public required Patient ExaminedPatient { get; set; }
        public required Doctor ResponsibleDoctor { get; set; }
        public required DateTimeOffset DateOfExamination { get; set; }

        [MinLength(10)]
        [MaxLength(250)]
        public required string ExaminationNotes { get; set; }

        public int DiagnosedIllnessId { get; set; }
        public required Illness DiagnosedIllness { get; set; }

        public required Treatment ProposedTreatment { get; set; }
    }
}
