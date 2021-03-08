using Microsoft.EntityFrameworkCore;

namespace PostStationAPI.Models
{
    public class StationContext : DbContext
    {
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Post> Posts { get; set; }

        public StationContext(DbContextOptions<StationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}