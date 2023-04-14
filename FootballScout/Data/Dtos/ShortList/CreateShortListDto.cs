using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.ShortList
{
    public record CreateShortListDto([Required] string Name);
}
