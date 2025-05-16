using Microsoft.EntityFrameworkCore;
using LaptimeBaseAPI.Models;

namespace LaptimeBaseAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Laptime> Laptimes { get; set; }
    }
}