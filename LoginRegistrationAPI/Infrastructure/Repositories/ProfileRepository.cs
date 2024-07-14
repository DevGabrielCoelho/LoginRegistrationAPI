using LoginRegistrationAPI.Application.ViewModels;
using LoginRegistrationAPI.Domain.Models.ProfileAggregate;
using LoginRegistrationAPI.Domain.Models.UserAggregate;

namespace LoginRegistrationAPI.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ConnectionContext _connectionContext = new();
        public void UpdateProfile(int id, ProfileViewModel profileViewModel)
        {
            if (!_connectionContext.Profiles.Any(x => x.Id == id)) throw new ArgumentException("O Profile não existe!");
            Profile profile = _connectionContext.Profiles.Where(x => x.Id == id).ToArray()[0];
            profile.PhoneNumber = profileViewModel.PhoneNumber;
            profile.BirthDate = profileViewModel.BirthDate;
            profile.FirstName = profileViewModel.FirstName;
            profile.LastName = profileViewModel.LastName;
            DateTime dateTime = DateTime.Now;
            profile.UpdatedAt = dateTime;
            User user = _connectionContext.Users.Where(x => x.Id == profile.UserId).ToArray()[0];
            user.UpdatedAt = dateTime;
            user.Profile = profile;
            profile.User = user;
            _connectionContext.Profiles.Update(profile);
            _connectionContext.Users.Update(user);
            _connectionContext.SaveChanges();
        }
    }
}
