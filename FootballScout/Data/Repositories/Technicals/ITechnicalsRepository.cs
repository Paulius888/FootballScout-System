using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories.Technicals
{
    public interface ITechnicalsRepository
    {
        Task Add(Technical technical);
        Task<Technical> Get(int playerId, int technicalId);
        Task<List<Technical>> GetAll(int playerId);
        Task Remove(Technical technical);
        Task Update(Technical technical);
    }
}