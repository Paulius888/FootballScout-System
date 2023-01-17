using AutoMapper;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Teams;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class AllTeamsController : ControllerBase
    {
        private readonly ITeamsRepository _teamsRepository;
        private readonly ILeaguesRepository _leaguesRepository;
        private readonly IMapper _mapper;

        public AllTeamsController(ITeamsRepository teamsRepository, IMapper mapper, ILeaguesRepository leaguesRepository)
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
            _leaguesRepository = leaguesRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<AllTeamsDto>> GetAll()
        {
            return (await _teamsRepository.GetAllTeams()).Select(o => _mapper.Map<AllTeamsDto>(o));
        }
    }
}