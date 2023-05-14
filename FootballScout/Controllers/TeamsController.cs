using AutoMapper;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Teams;
using FootballScout.Filter;
using FootballScout.Helpers;
using FootballScout.Services;
using FootballScout.Wrappers;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUriService uriService;

        public TeamsController(ITeamsRepository teamsRepository, IMapper mapper, ILeaguesRepository leaguesRepository, IUriService uriService)
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
            _leaguesRepository = leaguesRepository;
            this.uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult<TeamDto>> GetAll(int leagueId, [FromQuery] PaginationFilter filter, [FromQuery] string? query = null)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var team = await _teamsRepository.GetAll(leagueId, filter, query);
            var teamResponse = _mapper.Map<IList<TeamDto>>(team);
            var totalRecords = await _teamsRepository.TotalCount(leagueId, query);
            var pagedResponse = PaginationHelper.CreatePagedReponse<TeamDto>(teamResponse, validFilter, totalRecords, uriService, route);
            return Ok(pagedResponse);
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult<Response<TeamDto>>> Get(int leagueId, int teamId)
        {
            var team = await _teamsRepository.Get(leagueId, teamId);

            if (team == null) return NotFound();

            return Ok(new Response<TeamDto>(_mapper.Map<TeamDto>(team)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response<TeamDto>>> Add(int leagueId, CreateTeamDto teamDto)
        {
            var league = await _leaguesRepository.Get(leagueId);
            if (league == null) return NotFound($"Could not find a league with this id {leagueId}");

            var team = _mapper.Map<Team>(teamDto);
            team.LeagueId = leagueId;
            team.League_Name = league.Name;

            await _teamsRepository.Add(team);

            return Created($"/api/leagues/{leagueId}/teams/{team.Id}", new Response<TeamDto>(_mapper.Map<TeamDto>(team)));
        }

        [HttpPut("{teamId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response<TeamDto>>> Put(int leagueId, int teamId, UpdateTeamDto teamDto)
        {
            var league = await _leaguesRepository.Get(leagueId);
            if (league == null) return NotFound($"Could not find a league with this id {leagueId}");

            var oldTeam = await _teamsRepository.Get(leagueId, teamId);
            if (oldTeam == null) return NotFound();

            _mapper.Map(teamDto, oldTeam);

            await _teamsRepository.Update(oldTeam);

            return Ok(new Response<TeamDto>(_mapper.Map<TeamDto>(oldTeam)));
        }

        [HttpDelete("{teamId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int leagueId, int teamId)
        {
            var team = await _teamsRepository.Get(leagueId, teamId);
            if (team == null) return NotFound();

            await _teamsRepository.Remove(team);

            return NoContent();
        }
    }
}