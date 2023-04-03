using FootballScout.Data.Entities;

namespace FootballScout.Data.Dtos.Players
{
    public record AllPlayersDto(int Id, string Name, int Age, DateOnly Contract, int Wage, int Price, int CurrentAbility,
        int PotentialAbility, bool IsGoalKeeper, bool IsEuCitizen, string Personality, string[] Role,string League_Name, int LeagueId, string Team_Name, int TeamId);
}
