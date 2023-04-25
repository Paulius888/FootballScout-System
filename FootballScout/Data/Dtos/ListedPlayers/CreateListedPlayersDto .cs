using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.ListedPlayers
{
    public record CreateListedPlayersDto([Required] int PlayerId);
}
