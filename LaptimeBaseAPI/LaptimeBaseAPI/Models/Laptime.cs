using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptimeBaseAPI.Models
{
    [Table("laptime")]
    public class Laptime
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("time")]
        public required TimeSpan Time { get; set; }

        [Column("created_at")]
        public required DateTime CreatedAt { get; set; }
        
        public virtual Team Team { get; set; }
    }
}
