using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.Leagues
{
        public record UpdateLeagueDto([Required] string Name);
}