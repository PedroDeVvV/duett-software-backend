using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ApiDuettSoftware.Model;
using System.IdentityModel.Tokens.Jwt;

namespace ApiDuettSoftware.Services
{
    public class TokenService
    {
        public static object GenerateToken(Person person)
        {
            var key = Encoding.UTF8.GetBytes(ApiDuettSoftware.Key.Secret);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, person.Id.ToString()),
            new Claim(ClaimTypes.Name, person.Name),
            new Claim(ClaimTypes.Email, person.Email),
            new Claim(ClaimTypes.Role, person.Type.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new { tokenString };
        }
    }
}
