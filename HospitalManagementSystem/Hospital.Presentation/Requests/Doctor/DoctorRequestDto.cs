using System.ComponentModel.DataAnnotations;

namespace Hospital.Presentation.Dto.Doctor
{
    public class DoctorRequestDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public required string Surname { get; set; }

        [Required]
        [MinLength(5)]
        public required string Address { get; set; }

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }

        [Required]
        [Range(1, 30)]
        public required int DepartmentId { get; set; }

        [Required]
        [RegularExpression(@"^(\d+\.?\d*):(\d+\.?\d*)$")]
        public required string StartShift { get; set; }

        [Required]
        [RegularExpression(@"^(\d+\.?\d*):(\d+\.?\d*)$")]
        public required string EndShift { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(7)]
        public required List<int> WeekDayIds { get; set; }
    }
}
