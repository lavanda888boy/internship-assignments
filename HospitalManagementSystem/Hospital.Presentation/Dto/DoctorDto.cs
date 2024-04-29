using System.ComponentModel.DataAnnotations;

namespace Hospital.Presentation.Dto
{
    public class DoctorDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public required string Surname { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }

        [Required]
        [Range(1, 30)]
        public required int DepartmentId { get; set; }

        [Required]
        public required TimeSpan StartShift { get; set; }

        [Required]
        public required TimeSpan EndShift { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(7)]
        public required List<int> WeekDayIds { get; set; }
    }
}
