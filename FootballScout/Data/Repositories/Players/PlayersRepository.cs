using FootballScout.Data.Entities;
using FootballScout.Filter;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data.Repositories.Players
{
    public class PlayersRepository : IPlayersRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PlayersRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Player>> GetAllPlayers(PaginationFilter filter, string query = null)
        {
            var queryable = Search(query);
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await queryable
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
        }

        public async Task<List<Player>> GetAllFieldPlayers(PaginationFilter filter, string query = null)
        {
            var queryable = SearchName(query);
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await queryable
                .Where(o => o.IsGoalKeeper == false)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
        }

        public async Task<List<Player>> GetAllGoalKeepingPlayers(PaginationFilter filter, string query = null)
        {
            var queryable = SearchName(query);
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await queryable
                .Where(o => o.IsGoalKeeper == true)
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

        public async Task<int> TotalCount(int teamId, string query = null)
        {
            var queryable = Search(query);
            int totalCount = queryable.Where(o => o.TeamId == teamId).Count();
            return totalCount;
        }

        public async Task<List<Player>> GetAll(int teamId, PaginationFilter filter, string query = null)
        {
            var queryable = Search(query);
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await queryable.Where(o => o.TeamId == teamId)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
        }

        public async Task<Player> Get(int playerId)
        {
            return await _databaseContext.Player.FirstOrDefaultAsync(o => o.Id == playerId);
        }

        public async Task Add(Player player)
        {
            _databaseContext.Player.Add(player);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(Player player)
        {
            _databaseContext.Player.Update(player);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Remove(Player player)
        {
            _databaseContext.Player.Remove(player);
            await _databaseContext.SaveChangesAsync();
        }

        private IQueryable<Player> Search(string query)
        {
            var queryable = _databaseContext.Player.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(query) || x.Name.Contains(query)
                || x.Personality.ToLower().Contains(query) || x.Personality.Contains(query)
                || x.Team_Name.ToLower().Contains(query) || x.Team_Name.Contains(query)
                || x.Role.Contains(query) || x.Age.ToString().Contains(query)
                || x.Wage.ToString().Contains(query) || x.Price.ToString().Contains(query)
                || x.CurrentAbility.ToString().Contains(query) || x.PotentialAbility.ToString().Contains(query));
            }

            return queryable;
        }

        private IQueryable<Player> SearchName(string query)
        {
            var queryable = _databaseContext.Player.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(query) || x.Name.Contains(query));
            }

            return queryable;
        }
    }
}
