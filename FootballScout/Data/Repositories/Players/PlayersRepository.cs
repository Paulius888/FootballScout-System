using FootballScout.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data.Repositories.Players
{
    public class PlayersRepository : IPlayersRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PlayersRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Player>> GetAll(int teamId)
        {
            return await _databaseContext.Player.Where(o => o.TeamId == teamId).ToListAsync();
        }

        public async Task<Player> Get(int teamId, int playerId)
        {
            return await _databaseContext.Player.FirstOrDefaultAsync(o => o.TeamId == teamId && o.Id == playerId);
        }

        public async Task Add(Player player)
        {
            _databaseContext.Player.Add(player);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(Player player)
        {
            _databaseContext.Player.Update(player);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Remove(Player player)
        {
            _databaseContext.Player.Remove(player);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
