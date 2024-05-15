using System.Security.Claims;

namespace Hospital.Application.Abstractions
{
    public interface IJwtGenerationService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims);
    }
}
