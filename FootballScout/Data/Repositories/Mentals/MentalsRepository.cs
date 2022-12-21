using FootballScout.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data.Repositories.Mentals
{
    public class MentalsRepository : IMentalsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public MentalsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Mental>> GetAll(int playerId)
        {
            return await _databaseContext.Mental.Where(o => o.PlayerId == playerId).ToListAsync();
        }

        public async Task<Mental> Get(int playerId, int mentalId)
        {
            return await _databaseContext.Mental.FirstOrDefaultAsync(o => o.PlayerId == playerId && o.Id == mentalId);
        }

        public async Task Add(Mental mental)
        {
            _databaseContext.Mental.Add(mental);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(Mental mental)
        {
            _databaseContext.Mental.Update(mental);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Remove(Mental mental)
        {
            _databaseContext.Mental.Remove(mental);
            await _databaseContext.SaveChangesAsync();
        }
    }
}