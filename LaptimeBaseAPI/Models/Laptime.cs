using System.ComponentModel.DataAnnotations.Schema;

namespace LaptimeBaseAPI.Models
{
    [Table("laptime")]
    public class Laptime
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("time")]
        public TimeSpan Time { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("team_id")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        [Column("session_id")]
        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
