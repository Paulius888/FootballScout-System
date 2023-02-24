using FootballScout.Data.Entities;
using FootballScout.Filter;

namespace FootballScout.Data.Repositories.Teams
{
    public interface ITeamsRepository
    {
        Task<List<Team>> GetAllTeams(PaginationFilter filter, string query = null);
        Task<int> TotalCount(string query = null);
        Task Add(Team team);
        Task<Team> Get(int leagueId, int teamId);
        Task<List<Team>> GetAll(int leagueId);
        Task Remove(Team team);
        Task Update(Team team);
    }
}