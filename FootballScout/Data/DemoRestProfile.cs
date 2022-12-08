using AutoMapper;
using FootballScout.Data.Dtos.Leagues;
using FootballScout.Data.Entities;

namespace FootballScout.Data
{
    public class DemoRestProfile : Profile
    {
        public DemoRestProfile() 
        {
            CreateMap<League, LeagueDto>();
            CreateMap<CreateLeagueDto, League>();
            CreateMap<UpdateLeagueDto, League>();
        }
    }
}
