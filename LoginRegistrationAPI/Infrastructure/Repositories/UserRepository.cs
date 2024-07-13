using LoginRegistrationAPI.Domain.Models.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace LoginRegistrationAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public List<User> GetAll()
        {
            return _context.Users.Include(u => u.Profile).Include(u => u.Profile.Sessions).ToList();
        }

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
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    // Certifique-se de que o Profile seja adicionado ao contexto apenas se for novo
                    if (user.Profile != null && user.Profile.Id == 0)
                    {
                        _context.Profiles.Add(user.Profile);
                        _context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao registrar usuário", ex);
                }
            }
        }

        public void UpdateProfile()
        {
            throw new NotImplementedException();
        }
    }
}
