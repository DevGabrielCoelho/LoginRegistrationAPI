using LoginRegistrationAPI.Domain.Models.ProfileAggregate;
using LoginRegistrationAPI.Domain.Models.SessionsAggregate;
using LoginRegistrationAPI.Domain.Models.UserAggregate;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginRegistrationAPI.Application.Services
{
    public class TokenService
    {
        public static object GenerateToken(User user, Profile profile, Session session)
        {
            byte[] key = Encoding.ASCII.GetBytes(Key.Secret);
            SecurityTokenDescriptor tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                      new Claim("userId", user.Id.ToString()),
                      new Claim("profileId", profile.Id.ToString()),
                      new Claim("sessionId", session.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenConfig);
            string tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }
    }
}
