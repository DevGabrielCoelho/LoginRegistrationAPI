using LoginRegistrationAPI.Application.ViewModels;
using LoginRegistrationAPI.Domain.Models.ProfileAggregate;
using LoginRegistrationAPI.Domain.Models.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

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
                    
                    if (user == null) throw new ArgumentNullException(nameof(user));
                    if (_context.Users.Any(x => x.Email == user.Email)) 
                        throw new InvalidOperationException(nameof(user.Email));
                    if (_context.Users.Any(x => x.Profile.PhoneNumber == user.Profile.PhoneNumber)) 
                        throw new InvalidDataException(nameof(user.Profile.PhoneNumber));
                    
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    if (user.Profile != null && user.Profile.Id == 0)
                    {
                        _context.Profiles.Add(user.Profile);
                        _context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (ArgumentNullException ex)
                {
                    transaction.Rollback();
                    throw new Exception("As informações não podem ser nulas", ex);
                }
                catch (InvalidOperationException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Esse email já foi utilizado!", ex);
                }
                catch (InvalidDataException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Esse numero já foi utilizado!", ex);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao registrar usuário", ex);
                }
            }
        }

    }
}
