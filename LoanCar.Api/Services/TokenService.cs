using LoanCar.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoanCar.Api.Services
{
    public class TokenService
    {
        private readonly SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("FIXMEFIXMEFIXMEFIXMEFIXMEFIXMEFIXME"));

        public string NewToken(User user)
        {
            var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new("admin", user.IsAdmin.ToString()),
            new("email", user.Email),
        };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
