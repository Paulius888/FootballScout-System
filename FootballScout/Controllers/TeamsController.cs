using AutoMapper;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Teams;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/leagues/{leagueId}/teams")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsRepository _teamsRepository;
        private readonly ILeaguesRepository _leaguesRepository;
        private readonly IMapper _mapper;

        public TeamsController(ITeamsRepository teamsRepository, IMapper mapper, ILeaguesRepository leaguesRepository) 
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
            _leaguesRepository = leaguesRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TeamDto>> GetAll(int leagueId)
        {
            var leagues = await _teamsRepository.GetAll(leagueId);
            return leagues.Select(o => _mapper.Map<TeamDto>(o));
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult<TeamDto>> Get(int leagueId, int teamId)
        {
            var league = await _teamsRepository.Get(leagueId, teamId);

            if(league == null) return NotFound();

            return Ok(_mapper.Map<TeamDto>(league));
        }

        [HttpPost]
        public async Task<ActionResult<TeamDto>> Add (int leagueId, CreateTeamDto teamDto)
        {
            var league = await _leaguesRepository.Get(leagueId);
            if (league == null) return NotFound($"Could not find a league with this id {leagueId}");

            var team = _mapper.Map<Team>(teamDto);
            team.LeagueId = leagueId;

            await _teamsRepository.Add(team);

            return Created($"/api/leagues/{leagueId}/teams/{team.Id}", _mapper.Map<TeamDto>(team));
        }

        [HttpPut("{teamId}")]
        public async Task<ActionResult<TeamDto>> Put(int leagueId, int teamId, UpdateTeamDto teamDto)
        {
            var league = await _leaguesRepository.Get(leagueId);
            if (league == null) return NotFound($"Could not find a league with this id {leagueId}");

            var oldTeam = await _teamsRepository.Get(leagueId, teamId);
            if (oldTeam == null) return NotFound();

            _mapper.Map(teamDto, oldTeam);

            await _teamsRepository.Update(oldTeam);

            return Ok(_mapper.Map<TeamDto>(oldTeam));
        }

        [HttpDelete("{teamId}")]
        public async Task<ActionResult> Delete (int leagueId, int teamId)
        {
            var team = await _teamsRepository.Get(leagueId,teamId);
            if (team == null) return NotFound();

            await _teamsRepository.Remove(team);

            return NoContent();
        }
    }
}