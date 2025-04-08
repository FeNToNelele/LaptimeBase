using Microsoft.EntityFrameworkCore;
using LaptimeBaseAPI.Models;

namespace LaptimeBaseAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Laptime> Laptimes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite primary key for Team (UserId + CarId)
            modelBuilder.Entity<Team>()
                .HasKey(t => new { t.UserId, t.CarId });

            modelBuilder.Entity<Team>()
                .HasOne(t => t.User)
                .WithMany(u => u.Teams)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Car)
                .WithMany(c => c.Teams)
                .HasForeignKey(t => t.CarId);

            modelBuilder.Entity<Laptime>()
                .HasOne(lt => lt.Team)
                .WithMany(t => t.Laptimes)
                .HasForeignKey(lt => lt.TeamId)
                .HasPrincipalKey(t => t.UserId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Track)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrackId);
        }
    }
}