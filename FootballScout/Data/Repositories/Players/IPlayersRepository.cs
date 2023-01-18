using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories.Players
{
    public interface IPlayersRepository
    {
        Task<List<Player>> GetAllPlayers();
        Task Add(Player player);
        Task<Player> Get(int teamId, int playerId);
        Task<List<Player>> GetAll(int teamId);
        Task Remove(Player player);
        Task Update(Player player);
    }
}