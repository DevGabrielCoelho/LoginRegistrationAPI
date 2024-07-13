namespace LoginRegistrationAPI.Domain.Models.SessionsAggregate
{
    public interface ISessionRepository
    {
        void CreateSession();
        void ValidateToken();
        void InvalidateToken();
    }
}
