using Hospital.Application.Abstractions;
using Hospital.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hospital.Infrastructure.Identity
{
    public class JwtGenerationService : IJwtGenerationService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtGenerationService(IOptions<JwtOptions> authenticationOptions)
        {
            _jwtOptions = authenticationOptions.Value;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var signinCredentials = new SigningCredentials(_jwtOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                 issuer: _jwtOptions.Issuer,
                 audience: _jwtOptions.Audiences[0],
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(_jwtOptions.TokenLifetime),
                 signingCredentials: signinCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);
            return encodedToken;
        }
    }
}
