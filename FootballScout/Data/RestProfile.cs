﻿using AutoMapper;
using FootballScout.Data.Dtos.Leagues;
using FootballScout.Data.Dtos.Mentals;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Dtos.Technicals;
using FootballScout.Data.Entities;

namespace FootballScout.Data
{
    public class RestProfile : Profile
    {
        public RestProfile() 
        {
            CreateMap<League, LeagueDto>();
            CreateMap<CreateLeagueDto, League>();
            CreateMap<UpdateLeagueDto, League>();

            CreateMap<CreateTeamDto, Team>();
            CreateMap<UpdateTeamDto, Team>();
            CreateMap<Team, TeamDto>();

            CreateMap<CreatePlayerDto, Player>();
            CreateMap<UpdatePlayerDto, Player>();
            CreateMap<Player, PlayerDto>();

            CreateMap<CreateTechnicalDto, Technical>();
            CreateMap<UpdateTechnicalDto, Technical>();
            CreateMap<Technical, TechnicalDto>();

            CreateMap<CreateMentalDto, Mental>();
            CreateMap<UpdateMentalDto, Mental>();
            CreateMap<Mental, MentalDto>();
        }
    }
}
