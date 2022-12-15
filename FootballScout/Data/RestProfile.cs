using AutoMapper;
using FootballScout.Data.Dtos.Leagues;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Dtos.Teams;
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
        }
    }
}
