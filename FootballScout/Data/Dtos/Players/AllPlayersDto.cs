using FootballScout.Data.Entities;

namespace FootballScout.Data.Dtos.Players
{
    public record AllPlayersDto(int Id, string Name, int Age, DateTime Contract, int Wage, int Price, int CurrentAbility,
        int PotentialAbility, bool IsGoalKeeper, bool IsEuCitizen, string Personality, string Team_Name);
}
