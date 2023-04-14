using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories.ListedPlayers
{
    public interface IListedPlayersRepository
    {
        Task<ListedPlayer> Add(int id, ListedPlayer listedPlayer);
        Task<Player[]> GetShortlistedPlayers(int shortlistId, Player[] array);
        Task Remove(ListedPlayer listedPlayer);
        Task<ListedPlayer> Get(int id);
        Task<List<ListedPlayer>> GetShortlistedPlayersCount(int shortlistId);
    }
}