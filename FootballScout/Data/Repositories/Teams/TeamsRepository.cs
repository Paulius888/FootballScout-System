using FootballScout.Data.Entities;
using FootballScout.Filter;
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

        public async Task<List<Team>> GetAllTeams(PaginationFilter filter, string query = null)
        {
            var queryable = Search(query);

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await queryable
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
        }

        public async Task<int> TotalCount(string query = null)
        {
            var queryable = Search(query);
            int totalCount = queryable.Count();
            return totalCount;
        }

        public async Task<int> TotalCount(int leagueId, string query = null)
        {
            var queryable = Search(query);
            int totalCount = queryable.Where(o => o.LeagueId == leagueId).Count();
            return totalCount;
        }

        public async Task<List<Team>> GetAll(int leagueId, PaginationFilter filter, string query = null)
        {
            var queryable = Search(query);
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await queryable.Where(o => o.LeagueId == leagueId)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
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

        private IQueryable<Team> Search(string query)
        {
            var queryable = _databaseContext.Team.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(query) || x.Name.Contains(query)
                || x.Training_Facilities.ToLower().Contains(query) || x.Training_Facilities.Contains(query)
                || x.Youth_Facilities.ToLower().Contains(query) || x.Youth_Facilities.Contains(query));
            }

            return queryable;
        }
    }
}