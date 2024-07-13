using LoginRegistrationAPI.Domain.Models.ProfileAggregate;
using LoginRegistrationAPI.Domain.Models.SessionsAggregate;
using LoginRegistrationAPI.Domain.Models.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;

namespace LoginRegistrationAPI.Infrastructure
{
    public class ConnectionContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Profile>()
                .HasOne(u => u.User)
                .WithOne(p => p.Profile)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Profile>()
                .HasMany(p => p.Sessions)
                .WithOne(s => s.Profile)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Session>()
                .HasOne(p => p.Profile)
                .WithMany(s => s.Sessions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySQL("server=localhost;port=3306;userid=developer;password=123qwer;database=loginregistrationapiappdb;");
    }
}
