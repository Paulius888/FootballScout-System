using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories.Mentals
{
    public interface IMentalsRepository
    {
        Task Add(Mental mental);
        Task<Mental> Get(int playerId, int mentalId);
        Task<List<Mental>> GetAll(int playerId);
        Task Remove(Mental mental);
        Task Update(Mental mental);
    }
}