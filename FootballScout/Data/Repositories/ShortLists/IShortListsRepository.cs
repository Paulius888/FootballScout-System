using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories.ShortLists
{
    public interface IShortListsRepository
    {
        Task<ShortList> Add(ShortList shortList);
        Task<ShortList> Get(int id);
        Task<List<ShortList>> GetAllUserShortlists(string userId);
        Task Remove(ShortList shortList);
        Task Update(ShortList shortList);
    }
}