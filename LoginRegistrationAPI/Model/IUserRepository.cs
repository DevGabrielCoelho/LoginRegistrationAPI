namespace LoginRegistrationAPI.Model
{
    public interface IUserRepository
    {
        void Register(User user);
        void Login();
        void Logout();
        void UpdateProfile();
    }
}
