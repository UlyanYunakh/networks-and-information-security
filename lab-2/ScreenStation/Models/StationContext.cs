using Microsoft.EntityFrameworkCore;

namespace ScreenStation.Models
{
    public class StationContext : DbContext
    {
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Screenshot> Posts { get; set; }

        public StationContext(DbContextOptions<StationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}