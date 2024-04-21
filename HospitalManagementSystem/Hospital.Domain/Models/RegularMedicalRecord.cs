using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class RegularMedicalRecord
    {
        [Column("RecordId")]
        public required int Id { get; set; }

        public required Patient ExaminedPatient { get; set; }
        public required Doctor ResponsibleDoctor { get; set; }
        public required DateTimeOffset DateOfExamination { get; set; }

        [MinLength(10)]
        [MaxLength(250)]
        public required string ExaminationNotes { get; set; }
    }
}
