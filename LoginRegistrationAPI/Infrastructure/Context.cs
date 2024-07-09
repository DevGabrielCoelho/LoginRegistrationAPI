using LoginRegistrationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LoginRegistrationAPI.Infrastructure
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySQL("server=localhost;port=3306;userid=developer;password=123qwer;database=loginregistrationappdb;");
    }
}
