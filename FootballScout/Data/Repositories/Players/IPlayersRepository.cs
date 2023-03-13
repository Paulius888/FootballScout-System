using FootballScout.Data.Entities;
using FootballScout.Filter;

namespace FootballScout.Data.Repositories.Players
{
    public interface IPlayersRepository
    {
        Task<List<Player>> GetAllPlayers(PaginationFilter filter, string query = null);
        Task<int> TotalCount(string query = null);
        Task<int> TotalCount(int teamId, string query = null);
        Task Add(Player player);
        Task<Player> Get(int teamId, int playerId);
        Task<List<Player>> GetAll(int teamId, PaginationFilter filter, string query = null);
        Task Remove(Player player);
        Task Update(Player player);
    }
}