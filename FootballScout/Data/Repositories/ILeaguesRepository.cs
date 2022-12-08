using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories
{
    public interface ILeaguesRepository
    {
        Task<League> Create(League league);
        Task<League> Delete();
        Task<League> Get(int id);
        Task<IEnumerable<League>> GetAll();
        Task<League> Put();
    }
}