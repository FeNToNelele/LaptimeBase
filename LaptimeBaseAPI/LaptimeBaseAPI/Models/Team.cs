using System.ComponentModel.DataAnnotations.Schema;

namespace LaptimeBaseAPI.Models
{
    [Table("team")]
    public class Team
    {
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("car_id")]
        public int CarId { get; set; }
        public Car Car { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        public ICollection<Laptime> Laptimes { get; set; } = new List<Laptime>();
    }
}