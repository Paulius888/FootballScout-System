﻿using FootballScout.Data.Entities;

namespace FootballScout.Data.Dtos.Players
{
    public record UpdatePlayerDto (string Name, DateTime Contract, int Wage, int Price, int CurrentAbility,
        int PotentialAbility, bool IsGoalKeeper, bool IsEuCitizen, PersonalityEnum Personality);
}
