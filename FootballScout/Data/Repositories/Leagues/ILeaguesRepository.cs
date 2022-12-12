using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories.Leagues
{
    public interface ILeaguesRepository
    {
        Task<League> Add(League league);
        Task Remove(League league);
        Task<League> Get(int id);
        Task<IEnumerable<League>> GetAll();
        Task Update(League league);
    }
}