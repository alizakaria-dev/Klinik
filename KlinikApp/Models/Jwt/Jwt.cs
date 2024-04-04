using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Shared.Jwt
{
    public class Jwt
    {
        private JwtConfig _jwtConfig;
        public Jwt(IConfiguration configuration)
        {
            _jwtConfig = configuration.GetSection("JwtConfig").Get<JwtConfig>();
        }

        public string GenerateToken(string userName, int roleId, int userId)
        {
            var claims = new Claim[]
            {
                new Claim("UserId", userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, userName),
                new Claim("Role",roleId.ToString())
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey
                (System.Text.Encoding.UTF8.GetBytes(_jwtConfig.SecretKey)),
                SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(_jwtConfig.Issuer, _jwtConfig.Audience, claims, null, DateTime.UtcNow.AddHours(1), signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public JwtSecurityToken ReadJWTToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var tokenValue = handler.ReadJwtToken(token);

            return tokenValue;
        }
    }
}
