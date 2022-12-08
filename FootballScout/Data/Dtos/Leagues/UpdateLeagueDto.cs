using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.Leagues
{
    public class UpdateLeagueDto
    {
        public record CreateLeagueDto([Required] string Name);
    }
}
