using FootballScout.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data.Repositories.Physicals
{
    public class PhysicalsRepository : IPhysicalsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PhysicalsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Physical>> GetAll(int playerId)
        {
            return await _databaseContext.Physical.Where(o => o.PlayerId == playerId).ToListAsync();
        }

        public async Task<Physical> Get(int playerId, int physicalId)
        {
            return await _databaseContext.Physical.FirstOrDefaultAsync(o => o.PlayerId == playerId && o.Id == physicalId);
        }

        public async Task Add(Physical physical)
        {
            _databaseContext.Physical.Add(physical);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(Physical physical)
        {
            _databaseContext.Physical.Update(physical);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Remove(Physical physical)
        {
            _databaseContext.Physical.Remove(physical);
            await _databaseContext.SaveChangesAsync();
        }
    }
}