using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CairoGo.Repository.Implementations
{
    public class JwtTokenRepository: IJwtTokenRepository
    {
        private readonly IConfiguration _config;

        public JwtTokenRepository(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(UserApplication user)
        {
            var key = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_config["Jwt:Key"])
          );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Add unique token ID
            new Claim("FullName", user.FullName ?? ""),
            new Claim("UserId", user.Id.ToString()) //  Easier to access
        };

            // Use config value instead of hardcoded
            var durationInMinutes = int.Parse(_config["Jwt:DurationInMinutes"] ?? "120");

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
