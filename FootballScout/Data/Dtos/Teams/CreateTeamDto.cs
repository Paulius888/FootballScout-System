using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.Teams
{
    public record CreateTeamDto([Required]string Name, [Required] string Training_Facilities, [Required] string Youth_Facilities);
}