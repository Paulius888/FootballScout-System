using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.Physicals
{
    public record CreatePhysicalDto([Required] int Acceleration, [Required] int Agility, [Required] int Balance,
        [Required] int JumpingReach, [Required] int NaturalFitness,[Required] int Pace, [Required] int Stamina,
        [Required] int Strength);
}