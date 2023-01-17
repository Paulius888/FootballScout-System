using FootballScout.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data.Repositories.Teams
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly DatabaseContext _databaseContext;
        public TeamsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Team>> GetAllTeams()
        {
            return await _databaseContext.Team.ToListAsync();
        }

        public async Task<List<Team>> GetAll(int leagueId)
        {
            return await _databaseContext.Team.Where(o => o.LeagueId == leagueId).ToListAsync();
        }

        public async Task<Team> Get(int leagueId, int teamId)
        {
            return await _databaseContext.Team.FirstOrDefaultAsync(o => o.LeagueId == leagueId && o.Id == teamId);
        }

        public async Task Add(Team team)
        {
            _databaseContext.Team.Add(team);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(Team team)
        {
            _databaseContext.Team.Update(team);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Remove(Team team)
        {
            _databaseContext.Team.Remove(team);
            await _databaseContext.SaveChangesAsync();
        }
    }
}