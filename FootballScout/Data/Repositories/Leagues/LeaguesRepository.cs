using FootballScout.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FootballScout.Data.Repositories.Leagues
{
    public class LeaguesRepository : ILeaguesRepository
    {
        private readonly DatabaseContext _databaseContext;
        public LeaguesRepository(DatabaseContext databaseContext) 
        {
            _databaseContext = databaseContext;
        }
        public async Task<IEnumerable<League>> GetAll()
        {
            return new List<League>
            {
                new League()
                {
                    Name = "Name",
                    Nation = "Nation"
                },
                new League()
                {
                    Name = "Name",
                    Nation = "Nation"
                }
            };
        }

        public async Task<League> Get(int id)
        {
            return new League()
            {
                Name = "Name",
                Nation = "Nation"
            };
        }

        public async Task<League> Create(League league)
        {
            _databaseContext.League.Add(league);
            await _databaseContext.SaveChangesAsync();

            return league;
        }

        public async Task<League> Put(League league)
        {
            return new League()
            {
                Name = "Name",
                Nation = "Nation"
            };
        }

        public async Task Delete(League league)
        {
        }
    }
}
