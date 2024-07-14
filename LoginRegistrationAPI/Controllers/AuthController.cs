using Isopoh.Cryptography.Argon2;
using LoginRegistrationAPI.Application.Services;
using LoginRegistrationAPI.Domain.Models.SessionsAggregate;
using LoginRegistrationAPI.Domain.Models.UserAggregate;
using LoginRegistrationAPI.Infrastructure;
using LoginRegistrationAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace LoginRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        private readonly ConnectionContext _connectionContext = new ConnectionContext();
        private readonly ISessionRepository _sessionRepository;

        public AuthController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        [HttpPost]
        public IActionResult Auth(string email, string password)
        {
            bool verify = false;
            try
            {
                User user = _connectionContext.Users.Where(x => x.Email == email).ToArray()[0];
                if (Argon2.Verify(user.PasswordHash, password)) verify = true;
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

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string token)
        {
            int id = _connectionContext.Users.Where(x => x.Email == email).ToArray()[0].Id;
            bool result = _sessionRepository.CreateSession(id, token);
            if (!result) { return BadRequest("INVALID"); }
            return Ok();
        }

        [HttpPatch]
        [Route("logout")]
        public IActionResult Logout(string email)
        {
            int id = _connectionContext.Users.Where(x => x.Email == email).ToArray()[0].Id;
            _sessionRepository.InvalidateToken(id);
            return Ok();
        }

    }
}
