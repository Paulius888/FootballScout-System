using FootballScout.Data.Entities;

namespace FootballScout.Data.Dtos.Players
{
    public record UpdatePlayerDto (string Name, DateOnly Contract, int Wage, int Price, int CurrentAbility,
        int PotentialAbility, bool IsGoalKeeper, bool IsEuCitizen, string Personality);
}
