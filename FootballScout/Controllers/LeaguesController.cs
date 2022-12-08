using AutoMapper;
using FootballScout.Data.Dtos.Leagues;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/leagues")]
    public class LeaguesController : ControllerBase
    {
        private readonly ILeaguesRepository _leaguesRepository;
        private readonly IMapper _mapper;

        public LeaguesController(ILeaguesRepository leaguesRepository, IMapper mapper)
        {
            _leaguesRepository = leaguesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LeagueDto>> GetAll()
        {
            return (await _leaguesRepository.GetAll()).Select(o => _mapper.Map<LeagueDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeagueDto>> Get(int id)
        {
            var league = await _leaguesRepository.Get(id);
            if (league == null) return NotFound($"League'{id}' not found");

            return Ok(_mapper.Map<LeagueDto>(league));
        }
    }
}
