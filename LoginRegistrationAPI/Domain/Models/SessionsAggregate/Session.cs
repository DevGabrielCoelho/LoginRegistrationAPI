using LoginRegistrationAPI.Domain.Models.ProfileAggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegistrationAPI.Domain.Models.SessionsAggregate
{
    [Table("Session")]
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public Profile Profile { get; set; }
    }
}
