using FootballScout.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data.Repositories.ListedPlayers
{
    public class ListedPlayersRepository : IListedPlayersRepository
    {
        private readonly DatabaseContext _databaseContext;
        public ListedPlayersRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Player[]> GetShortlistedPlayers(int shortlistId, Player[] array)
        {
            var joinedValues = _databaseContext.ListedPlayer.Join(_databaseContext.Player,
                                                                 l => l.PlayerId,
                                                                 p => p.Id,
                                                                 (listed, player) => new
                                                                 {
                                                                     listedPlayer = listed.ShortListId,
                                                                     Player = player
                                                                 });
            if (joinedValues != null)
            {
                int i = 0;
                foreach (var player in joinedValues)
                {
                    if (player.listedPlayer == shortlistId)
                    {
                        array[i] = player.Player;
                        i++;
                    }
                }
            }

            return array;
            //return await _databaseContext.ListedPlayer.Where(o => o.ShortListId == shortlistId).ToListAsync();
        }

        public async Task<List<ListedPlayer>> GetShortlistedPlayersCount(int shortlistId)
        {
            return await _databaseContext.ListedPlayer.Where(o => o.ShortListId == shortlistId).ToListAsync();
        }

        public async Task<ListedPlayer> Get(int id)
        {
            return await _databaseContext.ListedPlayer.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<int> RetrieveId(int id)
        {
            var comparedId = 0;
            var joinedValues = _databaseContext.ListedPlayer.Join(_databaseContext.Player,
                                                                 l => l.PlayerId,
                                                                 p => p.Id,
                                                                 (listed, player) => new
                                                                 {
                                                                     listedPlayer = listed.Id,
                                                                     Player = player
                                                                 });
            if (joinedValues != null)
            {
                int i = 0;
                foreach (var player in joinedValues)
                {
                    if (player.Player.Id == id)
                    {
                        comparedId = player.listedPlayer;
                    }
                }
            }
            return comparedId;
        }

        public async Task<ListedPlayer> Add(int id, ListedPlayer listedPlayer)
        {
            var queryable = PlayerAlreadyExists(id, listedPlayer);

            if (queryable == null)
            {
                _databaseContext.ListedPlayer.Add(listedPlayer);
                await _databaseContext.SaveChangesAsync();
            }

            return listedPlayer;
        }

        public async Task Remove(ListedPlayer listedPlayer)
        {
            _databaseContext.ListedPlayer.Remove(listedPlayer);
            await _databaseContext.SaveChangesAsync();
        }

        private IQueryable<ListedPlayer> PlayerAlreadyExists(int id, ListedPlayer listedPlayer)
        {
            var StringifyPlayerId = listedPlayer.PlayerId.ToString();
            var StringifyId = id.ToString();
            var queryable = _databaseContext.ListedPlayer.AsQueryable();

            queryable = queryable.Where(x => x.PlayerId.ToString().Contains(StringifyPlayerId) && x.ShortListId.ToString().Contains(StringifyId));
            if (queryable.Count() <= 0)
            {
                return null;
            }
            return queryable;
        }
    }
}
