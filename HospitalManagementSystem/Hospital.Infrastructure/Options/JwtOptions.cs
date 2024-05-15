using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hospital.Infrastructure.Options
{
    public class JwtOptions
    {
        public string SigningKey { get; init; }
        public string Issuer { get; init; }
        public string[] Audiences { get; init; }
        public int TokenLifetime { get; init; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));
        }
    }
}
