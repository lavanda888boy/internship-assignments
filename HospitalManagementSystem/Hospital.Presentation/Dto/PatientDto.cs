using System.ComponentModel.DataAnnotations;

namespace Hospital.Presentation.Dto
{
    public class PatientDto
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
        [Range(1, 120)]
        public required int Age { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public required string Gender { get; set; }

        [Required]
        [MinLength(5)]
        public required string Address { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
        public string? InsuranceNumber { get; set; }
    }
}
