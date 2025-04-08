using System.ComponentModel.DataAnnotations.Schema;

namespace LaptimeBaseAPI.Models
{
    [Table("session")]
    public class Session
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("held_at")]
        public DateTime HeldAt { get; set; } = DateTime.UtcNow;
        [Column("ambient_temp")]
        public int AmbientTemp { get; set; }
        [Column("track_temp")]
        public int TrackTemp { get; set; }
        [Column("track_id")]
        public int TrackId { get; set; }
        public Track Track { get; set; }
        public ICollection<Laptime> Laptimes { get; set; } = new List<Laptime>();
    }
}