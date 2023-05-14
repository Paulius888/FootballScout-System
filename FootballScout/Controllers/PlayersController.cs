using AutoMapper;
using FootballScout.Authentication.Model;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Players;
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
    [Route("api/leagues/{leagueId}/teams/{teamId}/players")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly ITeamsRepository _teamsRepository;
        private readonly ILeaguesRepository _leaguesRepository;
        private readonly IMapper _mapper;
        private readonly IUriService uriService;

        public PlayersController(IPlayersRepository playersRepository, ITeamsRepository teamsRepository, IMapper mapper, ILeaguesRepository leaguesRepository, IUriService uriService)
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
            _leaguesRepository = leaguesRepository;
            _playersRepository = playersRepository;
            this.uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult<PlayerDto>> GetAll(int teamId, [FromQuery] PaginationFilter filter, [FromQuery] string? query = null)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var player = await _playersRepository.GetAll(teamId, filter, query);
            var playerResponse = _mapper.Map<IList<PlayerDto>>(player);
            var totalRecords = await _playersRepository.TotalCount(teamId, query);
            var pagedResponse = PaginationHelper.CreatePagedReponse<PlayerDto>(playerResponse, validFilter, totalRecords, uriService, route);
            return Ok(pagedResponse);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response<PlayerDto>>> Add(int leagueId, int teamId, CreatePlayerDto playerDto)
        {
            var team = await _teamsRepository.Get(leagueId, teamId);
            if (team == null) return NotFound($"Could not find a team with this id {teamId}");

            var player = _mapper.Map<Player>(playerDto);
            player.LeagueId = leagueId;
            player.League_Name = team.League_Name;
            player.TeamId = teamId;
            player.Team_Name = team.Name;

            await _playersRepository.Add(player);

            return Created($"/api/leagues/{leagueId}/teams/{team.Id}/players/{player.Id}", new Response<PlayerDto>(_mapper.Map<PlayerDto>(player)));
        }

        [HttpPut("{playerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response<PlayerDto>>> Put(int leagueId, int teamId, int playerId, UpdatePlayerDto playerDto)
        {
            var team = await _teamsRepository.Get(leagueId, teamId);
            if (team == null) return NotFound($"Could not find a team with this id {teamId}");

            var oldPlayer = await _playersRepository.Get( playerId);
            if (oldPlayer == null) return NotFound();

            _mapper.Map(playerDto, oldPlayer);

            await _playersRepository.Update(oldPlayer);

            return Ok(new Response<PlayerDto>(_mapper.Map<PlayerDto>(oldPlayer)));
        }

        [HttpDelete("{playerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int teamId, int playerId)
        {
            var player = await _playersRepository.Get( playerId);
            if (player == null) return NotFound();

            await _playersRepository.Remove(player);

            return NoContent();
        }
    }
}