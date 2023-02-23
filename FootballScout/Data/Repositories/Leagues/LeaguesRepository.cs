using FootballScout.Data.Entities;
using FootballScout.Filter;
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
        public async Task<IEnumerable<League>> GetAll(PaginationFilter filter, string query = null)
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

        private IQueryable<League> Search (string query)
        {
            var queryable = _databaseContext.League.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(query) || x.Nation.ToLower().Contains(query)
                || x.Name.Contains(query) || x.Nation.Contains(query));
            }

            return queryable;
        }
    }
}
