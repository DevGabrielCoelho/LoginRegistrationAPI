using Isopoh.Cryptography.Argon2;
using LoginRegistrationAPI.Application.Services;
using LoginRegistrationAPI.Domain.Models.CredentialAggregate;
using LoginRegistrationAPI.Domain.Models.UserAggregate;
using LoginRegistrationAPI.Infrastructure;
using LoginRegistrationAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LoginRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        private readonly ConnectionContext _connectionContext = new ConnectionContext();

        [HttpPost]
        public IActionResult Auth(string email, string password)
        {
            Credentials credentials = new Credentials(email, password);
            bool verify = false;
            try
            {
                User user = _connectionContext.Users.Where(x => x.Email == credentials.Email).ToArray()[0];
                if (Argon2.Verify(user.PasswordHash, credentials.Password)) verify = true;
            }
            catch
            {
                verify = false;
            }

            if (verify)
            {
                Object token = TokenService.GenerateToken(new Domain.Models.UserAggregate.User(),
                    new Domain.Models.ProfileAggregate.Profile(),
                    new Domain.Models.SessionsAggregate.Session());
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }
    }
}
