﻿using FootballScout.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.Players
{
    public record PlayerDto (int Id, string Name, int Age, DateTime Contract, int Wage, int Price, int CurrentAbility,
        int PotentialAbility, bool IsGoalKeeper,bool IsEuCitizen, PersonalityEnum Personality);
}