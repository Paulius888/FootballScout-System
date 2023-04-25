using FootballScout.Data.Entities;
using FootballScout.Filter;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data.Repositories.ShortLists
{
    public class ShortListsRepository : IShortListsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ShortListsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<ShortList>> GetAllUserShortlists(string userId, PaginationFilter filter, string query = null)
        {
            var queryable = Search(query);

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            return await queryable.Where(o => o.UserId == userId)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
        }

        public async Task<int> TotalCount(string userId, string query = null)
        {
            var queryable = Search(query);
            int totalCount = queryable.Where(o => o.UserId == userId).Count();
            return totalCount;
        }

        public async Task<ShortList> Get(int id)
        {
            return await _databaseContext.ShortList.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<ShortList> Add(ShortList shortList)
        {
            _databaseContext.ShortList.Add(shortList);
            await _databaseContext.SaveChangesAsync();

            return shortList;
        }

        public async Task Update(ShortList shortList)
        {
            _databaseContext.ShortList.Update(shortList);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Remove(ShortList shortList)
        {
            _databaseContext.ShortList.Remove(shortList);
            await _databaseContext.SaveChangesAsync();
        }

        private IQueryable<ShortList> Search(string query)
        {
            var queryable = _databaseContext.ShortList.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(query) || x.Name.Contains(query));
            }

            return queryable;
        }
    }
}
