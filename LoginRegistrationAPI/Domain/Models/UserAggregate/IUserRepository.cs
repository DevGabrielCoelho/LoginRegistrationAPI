namespace LoginRegistrationAPI.Domain.Models.UserAggregate
{
    public interface IUserRepository
    {
        void Register(User user);
        void Login();
        void Logout();
        void UpdateProfile();
        List<User> GetAll();
    }
}
