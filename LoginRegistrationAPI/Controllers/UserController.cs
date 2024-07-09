using LoginRegistrationAPI.Model;
using LoginRegistrationAPI.ViewModel;
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
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpPost]
        public IActionResult Registration([FromForm] UserViewModel userViewModel)
        {
            User user = new User(userViewModel.Username, userViewModel.Email, userViewModel.Password);
            _userRepository.Register(user);
            return Ok();
        }
    }
}
