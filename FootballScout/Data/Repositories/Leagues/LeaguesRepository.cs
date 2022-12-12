using FootballScout.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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
            return await _databaseContext.League.ToListAsync();
        }

        public async Task<League> Get(int id)
        {
            return await _databaseContext.League.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<League> Add(League league)
        {
            _databaseContext.League.Add(league);
            await _databaseContext.SaveChangesAsync();

            return league;
        }

        public async Task Update(League league)
        {
            _databaseContext.League.Update(league);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Remove(League league)
        {
            _databaseContext.League.Remove(league);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
