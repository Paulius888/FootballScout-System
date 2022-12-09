using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories.Leagues
{
    public interface ILeaguesRepository
    {
        Task<League> Create(League league);
        Task Delete(League league);
        Task<League> Get(int id);
        Task<IEnumerable<League>> GetAll();
        Task<League> Put(League league);
    }
}