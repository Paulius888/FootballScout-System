using FootballScout.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) { }
        public DbSet<League> League { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Technical> Technical { get; set; }
        public DbSet<Mental> Mental { get; set; }
        public DbSet<Physical> Physical { get; set; }
        public DbSet<Goalkeeping> Goalkeeping { get; set; }

        protected override void OnModelCreating(ModelBuilder optionsBuilder)
        {
            optionsBuilder.UseSerialColumns();
        }
    }
}