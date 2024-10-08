using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using zaloclone_test.Configurations;
using zaloclone_test.Models;

namespace zaloclone_test.Utilities
{
    public class JwtAuthentication
    {
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(ConfigManager.gI().SecretKey); // Use a strong secret key

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Username", user.Username),
                    new Claim("UserID", user.UserId.ToString()),
                    new Claim("Phone", user.Phone.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("RoleID", user.RoleId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration
                Issuer = ConfigManager.gI().Issuer,
                Audience = ConfigManager.gI().Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
