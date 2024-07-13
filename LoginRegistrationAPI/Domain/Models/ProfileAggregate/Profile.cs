using LoginRegistrationAPI.Domain.Models.SessionsAggregate;
using LoginRegistrationAPI.Domain.Models.UserAggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegistrationAPI.Domain.Models.ProfileAggregate
{
    [Table("Profile")]
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User User { get; set; }
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
