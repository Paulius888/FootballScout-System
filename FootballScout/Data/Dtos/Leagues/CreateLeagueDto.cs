using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.Leagues
{   
    public record CreateLeagueDto([Required]string Name, [Required] string Nation);
}