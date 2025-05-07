using System.ComponentModel.DataAnnotations.Schema;

namespace LaptimeBaseAPI.Models
{
    [Table("session")]
    public class Session
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("held_at")]
        public required DateTime HeldAt { get; set; } = DateTime.UtcNow;
        [Column("ambient_temp")]
        public required int AmbientTemp { get; set; } = 23;
        [Column("track_temp")]
        public required int TrackTemp { get; set; } = 27;
        [Column("track_id")]
        public required int TrackId { get; set; }
        public Track Track { get; set; }
        public ICollection<Laptime> Laptimes { get; set; } = new List<Laptime>();
    }
}