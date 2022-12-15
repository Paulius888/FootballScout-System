using FootballScout.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<League> League { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Player> Player { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=FootballDB");
        }
    }
}