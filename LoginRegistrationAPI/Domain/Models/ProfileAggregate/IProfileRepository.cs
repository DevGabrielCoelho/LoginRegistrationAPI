using LoginRegistrationAPI.Application.ViewModels;

namespace LoginRegistrationAPI.Domain.Models.ProfileAggregate
{
    public interface IProfileRepository
    {
        void UpdateProfile(int id, ProfileViewModel profileViewModel);
    }
}
