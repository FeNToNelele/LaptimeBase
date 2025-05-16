using System.ComponentModel.DataAnnotations.Schema;

namespace LaptimeBaseAPI.Models
{
    [Table("track")]
    public class Track
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }
    }
}