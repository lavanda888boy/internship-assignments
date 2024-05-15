using System.ComponentModel.DataAnnotations;

namespace Hospital.Presentation.Requests.Auth
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; init; }

        [Required]
        public required string Password { get; init; }
    }
}
