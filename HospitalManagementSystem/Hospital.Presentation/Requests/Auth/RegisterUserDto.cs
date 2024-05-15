using System.ComponentModel.DataAnnotations;

namespace Hospital.Presentation.Requests.Auth
{
    public class RegisterUserDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public required string Name { get; init; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public required string Surname { get; init; }

        [Required]
        [EmailAddress]
        public required string Email { get; init; }

        [Required]
        public required string Password { get; init; }
    }
}
