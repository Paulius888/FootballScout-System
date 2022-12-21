using FootballScout.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data.Repositories.GoalKeeping
{
    public class GoalKeepingRepository : IGoalKeepingRepository
    {
        private readonly DatabaseContext _databaseContext;

        public GoalKeepingRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Goalkeeping>> GetAll(int playerId)
        {
            return await _databaseContext.Goalkeeping.Where(o => o.PlayerId == playerId).ToListAsync();
        }

        public async Task<Goalkeeping> Get(int playerId, int goalkeepingId)
        {
            return await _databaseContext.Goalkeeping.FirstOrDefaultAsync(o => o.PlayerId == playerId && o.Id == goalkeepingId);
        }

        public async Task Add(Goalkeeping goalkeeping)
        {
            _databaseContext.Goalkeeping.Add(goalkeeping);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(Goalkeeping goalkeeping)
        {
            _databaseContext.Goalkeeping.Update(goalkeeping);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Remove(Goalkeeping goalkeeping)
        {
            _databaseContext.Goalkeeping.Remove(goalkeeping);
            await _databaseContext.SaveChangesAsync();
        }
    }
}