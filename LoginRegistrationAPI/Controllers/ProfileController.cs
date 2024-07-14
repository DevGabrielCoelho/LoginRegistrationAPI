using LoginRegistrationAPI.Application.ViewModels;
using LoginRegistrationAPI.Domain.Models.ProfileAggregate;
using LoginRegistrationAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/v1/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        [HttpPatch]
        [Route("update")]
        public IActionResult Update(int id, [FromForm] ProfileViewModel profileViewModel)
        {
            _profileRepository.UpdateProfile(id, profileViewModel);
            return Ok();
        }
    }
}
