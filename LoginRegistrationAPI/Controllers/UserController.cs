using Isopoh.Cryptography.Argon2;
using LoginRegistrationAPI.Application.ViewModels;
using LoginRegistrationAPI.Domain.Models.ProfileAggregate;
using LoginRegistrationAPI.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("registration")]
        public IActionResult Registration([FromForm] UserViewModel userViewModel, [FromForm] ProfileViewModel profileViewModel)
        {
            DateTime dateTime = DateTime.UtcNow;
            Profile profile = new Profile()
            {
                FirstName = profileViewModel.FirstName,
                LastName = profileViewModel.LastName,
                BirthDate = profileViewModel.BirthDate,
                PhoneNumber = profileViewModel.PhoneNumber,
                CreatedAt = dateTime,
                UpdatedAt = dateTime
            };
            User user = new User()
            {
                Username = userViewModel.Username,
                Email = userViewModel.Email,
                PasswordHash = Argon2.Hash(userViewModel.Password),
                CreatedAt = dateTime,
                UpdatedAt = dateTime,
                Profile = profile
            };
            user.Profile.User = user;

            try
            { 
                _userRepository.Register(user); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll() 
        {
            return Ok(_userRepository.GetAll());
        }
    }
}
