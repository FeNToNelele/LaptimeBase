using System.ComponentModel.DataAnnotations.Schema;

namespace LaptimeBaseAPI.Models
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; } // Store hashed passwords only

        [Column("is_admin")]
        public bool IsAdmin { get; set; }

        public List<Team> Teams { get; set; }
    }
}
