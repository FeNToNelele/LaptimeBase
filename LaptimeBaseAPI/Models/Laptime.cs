using System.ComponentModel.DataAnnotations.Schema;

namespace LaptimeBaseAPI.Models
{
    [Table("laptime")]
    public class Laptime
    {
        [Column("id")]
        public required int Id { get; set; }
        [Column("time")]
        public required TimeSpan Time { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("team_id")]
        public required int TeamId { get; set; }
        public required Team Team { get; set; }
        [Column("session_id")]
        public required int SessionId { get; set; }
        public required Session Session { get; set; }
    }
}
