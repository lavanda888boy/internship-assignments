using System.ComponentModel.DataAnnotations;

namespace Hospital.Presentation.Dto.Record
{
    public class DiagnosisMedicalRecordRequestDto
    {
        [Required]
        [Range(1, 500)]
        public required int PatientId { get; set; }

        [Required]
        [Range(1, 500)]
        public required int DoctorId { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(2000)]
        public required string ExaminationNotes { get; set; }

        [Required]
        [Range(1, 500)]
        public required int IllnessId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public required string PrescribedMedicine { get; set; }

        [Required]
        [Range(1, 30)]
        public required int Duration { get; set; }
    }
}
