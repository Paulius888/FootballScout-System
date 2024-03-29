﻿using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.Players
{
    public record CreatePlayerDto([Required] string Name, [Required] int Age, [Required] DateOnly Contract, 
        [Required] int Wage, [Required] int Price, [Required] int CurrentAbility, [Required] int PotentialAbility,
        [Required] bool IsGoalKeeper, [Required] bool IsEuCitizen, [Required] string Personality, [Required] string[] Role);
}