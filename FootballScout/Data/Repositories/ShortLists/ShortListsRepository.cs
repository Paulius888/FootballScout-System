using FootballScout.Data.Entities;
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

        public async Task<List<ShortList>> GetAllUserShortlists(string userId)
        {
            return await _databaseContext.ShortList.Where(o => o.UserId == userId).ToListAsync();
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
    }
}
