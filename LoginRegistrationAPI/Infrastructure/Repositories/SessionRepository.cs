using LoginRegistrationAPI.Domain.Models.ProfileAggregate;
using LoginRegistrationAPI.Domain.Models.SessionsAggregate;
using LoginRegistrationAPI.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;

namespace LoginRegistrationAPI.Infrastructure.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ConnectionContext _connectionContext = new();
        public bool CreateSession(int userId, string token)
        {
            try
            {
                bool result = false;
                result = ValidateToken(token);
                if (result)
                {
                    DateTime dateTime = DateTime.UtcNow;
                    Profile profile = _connectionContext.Profiles.Where(x => x.UserId == userId).ToArray()[0];
                    User user = _connectionContext.Users.Where(x => x.Id == userId).ToArray()[0];
                    Session session = new Session()
                    {
                        CreatedAt = dateTime,
                        ExpiredAt = dateTime.AddHours(24),
                        UserId = userId,
                        Token = token
                    };
                    profile.Sessions.Add(session);
                    user.Profile = profile;
                    profile.User = user;
                    _connectionContext.Sessions.Add(session);
                    _connectionContext.Profiles.Update(profile);
                    _connectionContext.Users.Update(user);
                    _connectionContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
            return false;       
        }

        public void InvalidateToken(int userId)
        {
            Session session = _connectionContext.Sessions.Where(x => x.UserId == userId && x.Id == _connectionContext.Sessions.Count()).ToArray()[0];
            session.ExpiredAt = DateTime.UtcNow;
            session.Token = null;
            _connectionContext.Sessions.Update(session);
            _connectionContext.SaveChanges();
        }

        private bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            try { IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken); }
            catch { return false; }
            return true;
        }
        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(LoginRegistrationAPI.Key.Secret)) // The same key as the one that generate the token
            };
        }
    }
}
