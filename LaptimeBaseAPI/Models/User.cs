using System.ComponentModel.DataAnnotations.Schema;

namespace LaptimeBaseAPI.Models
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public required int Id { get; set; }
        [Column("username")]
        public required string Username { get; set; }

        [Column("password")]
        public required string Password { get; set; } // Store hashed passwords only

        [Column("is_admin")]
        public required bool IsAdmin { get; set; }

        public List<Team> Teams { get; set; }
    }
}
