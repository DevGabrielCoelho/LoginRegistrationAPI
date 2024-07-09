using LoginRegistrationAPI.Model;

namespace LoginRegistrationAPI.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context = new Context();
        public void Login()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateProfile()
        {
            throw new NotImplementedException();
        }
    }
}
