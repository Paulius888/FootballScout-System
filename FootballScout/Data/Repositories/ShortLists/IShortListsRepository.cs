using FootballScout.Data.Entities;
using FootballScout.Filter;

namespace FootballScout.Data.Repositories.ShortLists
{
    public interface IShortListsRepository
    {
        Task<ShortList> Add(ShortList shortList);
        Task<ShortList> Get(int id);
        Task<List<ShortList>> GetAllUserShortlists(string userId, PaginationFilter filter, string query = null);
        Task Remove(ShortList shortList);
        Task Update(ShortList shortList);
        Task<int> TotalCount(string userId, string query = null);
    }
}