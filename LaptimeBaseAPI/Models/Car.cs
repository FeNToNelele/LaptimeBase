using System.ComponentModel.DataAnnotations.Schema;

namespace LaptimeBaseAPI.Models
{
    [Table("car")]
    public class Car
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("class")]
        public string Class { get; set; } // GT3, GT4, etc.

        public ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}