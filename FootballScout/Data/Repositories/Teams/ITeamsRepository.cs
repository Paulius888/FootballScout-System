using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories.Teams
{
    public interface ITeamsRepository
    {
        Task<List<Team>> GetAllTeams();
        Task Add(Team team);
        Task<Team> Get(int leagueId, int teamId);
        Task<List<Team>> GetAll(int leagueId);
        Task Remove(Team team);
        Task Update(Team team);
    }
}