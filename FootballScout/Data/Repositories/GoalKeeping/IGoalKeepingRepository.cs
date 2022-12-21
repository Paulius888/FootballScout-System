using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories.GoalKeeping
{
    public interface IGoalKeepingRepository
    {
        Task Add(Goalkeeping goalkeeping);
        Task<Goalkeeping> Get(int playerId, int goalkeepingId);
        Task<List<Goalkeeping>> GetAll(int playerId);
        Task Remove(Goalkeeping goalkeeping);
        Task Update(Goalkeeping goalkeeping);
    }
}