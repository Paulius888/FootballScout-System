using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories.Physicals
{
    public interface IPhysicalsRepository
    {
        Task Add(Physical physical);
        Task<Physical> Get(int playerId, int physicalId);
        Task<List<Physical>> GetAll(int playerId);
        Task Remove(Physical physical);
        Task Update(Physical physical);
    }
}