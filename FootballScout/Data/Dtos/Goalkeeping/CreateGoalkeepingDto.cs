using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.Goalkeeping
{
    public record CreateGoalkeepingDto([Required] int AerialReach, [Required] int CommandOfArea, [Required] int Communication,
        [Required] int Eccentricity, [Required] int FirstTouch,[Required] int Handling, [Required] int Kicking,
        [Required] int OneOnOnes, [Required] int Passing, [Required] int Punching, [Required] int Reflexes, 
        [Required] int RushingOut, [Required] int Throwing);
}