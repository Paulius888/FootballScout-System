using AutoMapper;
using FootballScout.Data.Dtos.Auth;
using FootballScout.Data.Dtos.FieldStats;
using FootballScout.Data.Dtos.Goalkeeping;
using FootballScout.Data.Dtos.GoalStats;
using FootballScout.Data.Dtos.Leagues;
using FootballScout.Data.Dtos.ListedPlayers;
using FootballScout.Data.Dtos.Mentals;
using FootballScout.Data.Dtos.Physicals;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Dtos.ShortList;
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
            CreateMap<Team, AllTeamsDto>();

            CreateMap<CreatePlayerDto, Player>();
            CreateMap<UpdatePlayerDto, Player>();
            CreateMap<Player, PlayerDto>();
            CreateMap<Player, AllPlayersDto>();

            CreateMap<CreateTechnicalDto, Technical>();
            CreateMap<UpdateTechnicalDto, Technical>();
            CreateMap<Technical, TechnicalDto>();

            CreateMap<CreateMentalDto, Mental>();
            CreateMap<UpdateMentalDto, Mental>();
            CreateMap<Mental, MentalDto>();

            CreateMap<CreatePhysicalDto, Physical>();
            CreateMap<UpdatePhysicalDto, Physical>();
            CreateMap<Physical, PhysicalDto>();

            CreateMap<CreateGoalkeepingDto, Goalkeeping>();
            CreateMap<UpdateGoalkeepingDto, Goalkeeping>();
            CreateMap<Goalkeeping, GoalkeepingDto>();

            CreateMap<FieldStatsDto, FieldStats>();
            CreateMap<FieldStats, FieldStatsDto>();

            CreateMap<GoalStatsDto, GoalStats>();
            CreateMap<GoalStats, GoalStatsDto>();

            CreateMap<RestUser, UserDto>();

            CreateMap<CreateShortListDto, ShortList>();
            CreateMap<UpdateShortListDto, ShortList>();
            CreateMap<ShortList, ShortListDto>();

            CreateMap<CreateListedPlayersDto, ListedPlayer>();
            CreateMap<ListedPlayer, ListedPlayersDto>();
        }
    }
}