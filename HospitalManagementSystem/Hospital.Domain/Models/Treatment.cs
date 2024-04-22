using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class Treatment
    {
        [Column("TreatmentId")]
        public required int Id { get; set; }

        [MinLength(5)]
        [MaxLength(20)]
        public required string PrescribedMedicine { get; set; }
        public required TimeSpan Duration { get; set; }
    }
}
