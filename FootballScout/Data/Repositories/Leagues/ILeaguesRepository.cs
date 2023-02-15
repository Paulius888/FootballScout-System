using FootballScout.Data.Entities;
using FootballScout.Filter;

namespace FootballScout.Data.Repositories.Leagues
{
    public interface ILeaguesRepository
    {
        Task<League> Add(League league);
        Task Remove(League league);
        Task<League> Get(int id);
        Task<int> TotalCount();
        Task<IEnumerable<League>> GetAll(PaginationFilter filter);
        Task Update(League league);
    }
}