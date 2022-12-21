using FootballScout.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data.Repositories.Technicals
{
    public class TechnicalsRepository : ITechnicalsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public TechnicalsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Technical>> GetAll(int playerId)
        {
            return await _databaseContext.Technical.Where(o => o.PlayerId == playerId).ToListAsync();
        }

        public async Task<Technical> Get(int playerId, int technicalId)
        {
            return await _databaseContext.Technical.FirstOrDefaultAsync(o => o.PlayerId == playerId && o.Id == technicalId);
        }

        public async Task Add(Technical technical)
        {
            _databaseContext.Technical.Add(technical);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(Technical technical)
        {
            _databaseContext.Technical.Update(technical);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Remove(Technical technical)
        {
            _databaseContext.Technical.Remove(technical);
            await _databaseContext.SaveChangesAsync();
        }
    }
}