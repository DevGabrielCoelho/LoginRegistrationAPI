namespace LoginRegistrationAPI.Domain.Models.SessionsAggregate
{
    public interface ISessionRepository
    {
        bool CreateSession(int userId, string token);
        void InvalidateToken(int userId);
    }
}
