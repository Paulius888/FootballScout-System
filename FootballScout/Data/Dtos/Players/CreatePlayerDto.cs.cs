using System.ComponentModel.DataAnnotations;
using FootballScout.Data.Entities;

namespace FootballScout.Data.Dtos.Players
{
    public record CreatePlayerDto([Required] string Name, [Required] int Age, [Required] DateTime Contract, 
        [Required] int Wage, [Required] int Price, [Required] int CurrentAbility, [Required] int PotentialAbility,
        [Required] bool IsGoalKeeper, [Required] bool IsEuCitizen, [Required] PersonalityEnum Personality);
}