using System.Reflection.Emit;
using FootballScout.Data.Dtos.Auth;
using FootballScout.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data
{
    public class DatabaseContext : IdentityDbContext<RestUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) { }
        public DbSet<League> League { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Technical> Technical { get; set; }
        public DbSet<Mental> Mental { get; set; }
        public DbSet<Physical> Physical { get; set; }
        public DbSet<Goalkeeping> Goalkeeping { get; set; }
        public DbSet<FieldStats> FieldStats { get; set; }
        public DbSet<GoalStats> GoalStats { get; set; }
        public DbSet<ShortList> ShortList { get; set; }
        public DbSet<RestUser> RestUser { get; set; }
        public DbSet<ListedPlayer> ListedPlayer { get; set; }

        protected override void OnModelCreating(ModelBuilder optionsBuilder)
        {
            base.OnModelCreating(optionsBuilder);
            optionsBuilder.UseSerialColumns();
        }
    }
}