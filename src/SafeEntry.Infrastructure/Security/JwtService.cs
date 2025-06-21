using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SafeEntry.Application.Interfaces;

namespace SafeEntry.Infrastructure.Security
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _cfg;

        public JwtService(IConfiguration cfg)
        {
            _cfg = cfg;
        }


        public string GenerateToken(Guid userId, string email)
        {
            var expires = DateTime.UtcNow.AddHours(2);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,   userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            var token = new JwtSecurityToken(
                issuer: _cfg["Jwt:Issuer"],
                audience: _cfg["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime GetExpiration() => DateTime.UtcNow.AddHours(2);
    }
}
